using Application.Affiliates.DTO_s.session;
using Application.Interfaces.AffiliateInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/Affiliates")]
[ApiController]
public class AffiliateController(IAffiliateCommands AffiliateCommands, IAffiliateQueries AffiliateQueries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    [Authorize(policy:("ApprovedAdminOrsuperAdmin"))] 
    public async Task<ActionResult<IEnumerable<AffiliateSessionResponse>>> GetAll()
    {
        var result = await AffiliateQueries.GetAllAffiliates();

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

    [Authorize(policy: "ApprovedAffiliateOrSuperAdminOrAdmin")]
    public async Task<ActionResult<AffiliateSessionResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await AffiliateQueries.GetById(id);
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
    [Authorize(policy: "SuperAdminOnly")]

    public async Task<ActionResult<AffiliateSessionResponse>> Update(UpdateAffiliateSession request)
    {
        if (request.AffiliateRequest.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await AffiliateCommands.UpdateAffiliateAsnc(request);
        if (result.IsSuccess)
            return Ok(true);

        return NotFound(result.Error);
    }


  
    

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(policy: "SuperAdminOnly")]

    public async Task<ActionResult<bool>> DeleteAffiliate(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await AffiliateCommands.DeleteAffiliateAsnc(id);

        if (!result.IsSuccess)
            return BadRequest("Affiliate not deleted");
            
        return Ok(result.Value);
    }
}