using Application.Assisstants.Dto_s.session;
using Application.Interfaces.AssisstantInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/Assisstants")]
[ApiController]
public class AssisstantController(IAssisstantCommands AssisstantCommands, IAssisstantQueries AssisstantQueries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: "ApprovedAssisstantOrSuperAdminOrAdmin")]
    public async Task<ActionResult<IEnumerable<AssisstantSessionResponse>>> GetAll()
    {
        var result = await AssisstantQueries.GetAllAssisstants();

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
    [Authorize(policy: "ApprovedAssisstantOrSuperAdminOrAdmin")]

    public async Task<ActionResult<AssisstantSessionResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await AssisstantQueries.GetById(id);
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

    public async Task<ActionResult<AssisstantSessionResponse>> Update(UpdateAssisstantSession request)
    {
        if (request.AssisstantRequest.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await AssisstantCommands.UpdateAssisstantAsnc(request);
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

    public async Task<ActionResult<bool>> DeleteAssisstant(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await AssisstantCommands.DeleteAssisstantAsnc(id);

        if (!result.IsSuccess)
            return BadRequest("Assisstant not deleted");
            
        return Ok(result.Value);
    }
}