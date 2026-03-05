using Application.Delivery.DTOs;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.DeliveryInterfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/DeliveryIntegration")]

[ApiController]
public class DeliveryIntegrationController(IDeliveryIntegrationCommands commands, IDeliveryIntegrationQueries Queries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<IEnumerable<DeliveryIntegration>>> GetAll()
    {
        var result = await Queries.GetAllAsync();

        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [HttpGet("{id:int}",Name = "GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<DeliveryIntegration>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await Queries.GetByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

  
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<int>> Create(CreateDeliveryIntegrationRequest request)
    {
        var result = await commands.CreateDeliveryIntegrationAsync(request);
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, request);

        return BadRequest(result.Error);
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<DeliveryIntegrationResponse>> Update(UpdateDeliveryIntegrationRequest request)
    {
        if (request.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await commands.UpdateDeliveryIntegrationAsync(request);
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

    public async Task<ActionResult<bool>> DeleteDeliveryIntegration(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await commands.DeleteDeliveryIntegrationAsync(id);

        if (!result.IsSuccess)
            return BadRequest("Customer not deleted");
            
        return Ok(result.Value);
    }
}