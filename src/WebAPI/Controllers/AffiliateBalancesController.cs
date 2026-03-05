using Application.AffiliatesBalance.DTOs;
using Application.Interfaces.AffiliateBalanceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/affiliate-balances")]
public class AffiliateBalancesController(IAffiliateBalanceCommands commands, IAffiliateBalanceQueries queries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    [Authorize(policy:"ApprovedAdminOrsuperAdmin")]
    public async Task<ActionResult<IEnumerable<AffiliateBalanceResponse>>> GetAll()
    {
        var result = await queries.GetAllAffiliateBalancesAsync();

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
    [Authorize(policy:"ApprovedAdminOrsuperAdmin")]

    public async Task<ActionResult<AffiliateBalanceResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await queries.GetAffiliateBalanceByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "SuperAdminOnly")]

    public async Task<ActionResult<int>> Create(CreateAffiliateBalanceRequest request)
    {
        var result = await commands.CreateAffiliateBalanceAsync(request);
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
    [Authorize(Policy = "SuperAdminOnly")]

    public async Task<ActionResult<AffiliateBalanceResponse>> Update(UpdateAffiliateBalanceRequest request)
    {
        if (request.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await commands.UpdateAffiliateBalanceAsync(request);
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
    [Authorize(policy: "SuperAdminOnly")]
    public async Task<ActionResult<bool>> DeleteAffiliateBalanceAsync(int id)
    {
        if (id < 1)
            BadRequest("Invalid id"); 
        var result = await commands.DeleteAffiliateBalanceAsync(id);

        if (!result.IsSuccess)
            return BadRequest("affiliate not deleted");
            
        return Ok(result.Value);
    }
}