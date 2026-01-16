using Application.Admins.DTO_s.session;
using Application.Interfaces.AdminInterfaces;
using Application.Users.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[Route("api/Admins")]
[ApiController]
public class AdminController(IAdminCommands AdminCommands, IAdminQueries AdminQueries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    [Authorize(Policy = "ApprovedAdminOrsuperAdmin")]
    public async Task<ActionResult<IEnumerable<AdminSessionResponse>>> GetAll()
    {
        var result = await AdminQueries.GetAllAdmins();

        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [Authorize(Policy = "ApprovedAdminOrsuperAdmin")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminSessionResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await AdminQueries.GetById(id);
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

    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<AdminSessionResponse>> Update(UpdateAdminSession request)
    {
        if (request.AdminRequest.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await AdminCommands.UpdateAdminAsnc(request);
        if (result.IsSuccess)
            return Ok(result.Value); ;

        return NotFound(result.Error);
    }


    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<bool>> ChangeAdminAvaillabilityte(ChangeStatusRequest request )
    {
       
        if (request == null)
            return BadRequest("Invalid input.Please check the submitted data");

        var result = await AdminCommands.ChangeUserStatusAsync(request);
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

    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<ActionResult<bool>> DeleteAdmin(int id)
    {
        if (id < 1)
            return BadRequest("Invalid id"); ;
        var result = await AdminCommands.DeleteAdminAsnc(id);

        if (!result.IsSuccess)
            return BadRequest("Admin not deleted");
            
        return Ok(result.Value);
    }
}