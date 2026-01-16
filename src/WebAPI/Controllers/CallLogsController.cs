using Application.CallsLog.DTOs;

using Application.Interfaces.CallLogInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/CallLogs")]
[ApiController]
public class CallLogController(ICallLogCommands Commands, ICallLogQueries Queries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: "SuperAdminOnly")]

    public async Task<ActionResult<IEnumerable<CallLogrResponse>>> GetAll()
    {
        var result = await Queries.GetAllCallLogsAsync();

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
    [Authorize(policy: "SuperAdminOnly")]

    public async Task<ActionResult<CallLogrResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await Queries.GetCallLogByIDAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

   
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: "ApprovedAssisstantOnly")]

    public async Task<ActionResult<int>> Create(CreateCallLogRequest request)
    {
        var result = await Commands.CreatCallLogAsync(request);
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
    [Authorize(policy: "SuperAdminOnly")]
    public async Task<ActionResult<CallLogrResponse>> Update(UpdateCallLogRequest request)
    {
        if (request.Id <= 0) 
            return BadRequest("Invalid input.Please check the submitted data");
        
        var result = await Commands.UpdateCallLogAsync(request);
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
    public async Task<ActionResult<bool>> Delete(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await Commands.DeleteCallLogAsync(id);

        if (!result.IsSuccess)
            return BadRequest("CallLog not deleted");
            
        return Ok(result.Value);
    }
}