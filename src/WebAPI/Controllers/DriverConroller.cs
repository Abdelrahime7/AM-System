using Application.Drivers.DTO_s;
using Application.Drivers.DTO_s.session;
using Application.Interfaces.DriverInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/Drivers")]
[ApiController]
public class DriverController(IDriverCommands DriverCommands, IDriverQueries DriverQueries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<IEnumerable<DriverSessionResponse>>> GetAll()
    {
        var result = await DriverQueries.GetAllDrivers();

        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedDriverOrSuperAdminOrAdmin")]
    public async Task<ActionResult<DriverSessionResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await DriverQueries.GetById(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

  

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<DriverSessionResponse>> Update(UpdateDriverSession request)
    {
        if (request.DriverRequest.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await DriverCommands.UpdateDriverAsnc(request);
        if (result.IsSuccess)
            return Ok(true);

        return NotFound(result.Error);
    }


    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedDriverOrSuperAdminOrAdmin")]

    public async Task<ActionResult<bool>> ChangeDriverAvaillabilityte(ChangeAvailability availability)
    {
       
        if (availability == null)
            return BadRequest("Invalid input.Please check the submitted data");

        var result = await DriverCommands.ChangeDriverAvaillability(availability);
        if (result.IsSuccess)
            return Ok();

        return NotFound(result.Error);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<bool>> DeleteDriver(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await DriverCommands.DeleteDriverAsnc(id);

        if (!result.IsSuccess)
            return BadRequest("Driver not deleted");
            
        return Ok(result.Value);
    }
}