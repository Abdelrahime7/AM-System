using Application.Interfaces.RoleInterfaces;
using Application.Roles.DTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController(IRoleQueries roleQueries, IRoleCommands roleCommands)
    : ControllerBase
{
    private readonly IRoleCommands _roleCommands = roleCommands;
    private readonly IRoleQueries _roleQueries = roleQueries;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAll()
    {
        var result = await _roleQueries.GetAllRolesAsync();

        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await _roleQueries.GetRoleByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("by-type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleResponse>> GetByRoleType(UserRole roleType)
    {
        var result = await _roleQueries.GetByRoleTypeAsync(roleType);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
        }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateRoleRequest request)
    {
        var result = await _roleCommands.CreateRoleAsync(request);
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, request);

        return BadRequest(result.Error);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateRoleRequest request)
    {
        if (request.Id <= 0)
            return BadRequest("Invalid input. Please check the submitted data");

        var result = await _roleCommands.UpdateRoleAsync(request);
        if (result.IsSuccess)
            return Ok();

        return NotFound(result.Error);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        if (id < 1)
            return BadRequest("Invalid role ID");

        var result = await _roleCommands.DeleteRoleAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}