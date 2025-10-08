using Application.Interfaces.OrderInterfaces;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/Orders")]
[ApiController]
public class OrderController(IOrderCommands commands, IOrderQueries queries) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ResponseSession>>> GetAll()
    {
        var result = await queries.GetAllOrdersAsync();

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
    public async Task<ActionResult<ResponseSession>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await queries.GetOrderByIDAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreatOrderSession request)
    {
        var result = await commands.CreateOrderAsync(request);
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
    public async Task<ActionResult<ResponseSession>> Update(UpdateOrderSession request)
    {
        if (request == null)
            return BadRequest("Invalid input.Please check the submitted data");

        var result = await commands.UpdateOrderAsync(request);
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
    public async Task<ActionResult<bool>> Delete(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await commands.DeleteOrderAsync(id);

        if (!result.IsSuccess)
            return BadRequest("Order not deleted");

        return Ok(result.Value);
    }

    [HttpPatch("api/Orders/ChangeStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<bool>> ChangeOrderStatus(ChangeOrderStatus request)
    {
        if (request.Id < 1)
            return NoContent();
        var result = await commands.ChangeOrderStatusAsync(request);

        if (!result.IsSuccess)
            return BadRequest("Customer not deleted");

        return Ok(result.Value);
    }

   
    [HttpPatch("api/Orders/AssignDelivery")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<bool>> AssignOrderToDelivery(Order order)
    {
        if (order.Id < 1)
            return NoContent();
        var result = await commands.AssignOrderToDelivery(order);

        if (!result.IsSuccess)
            return BadRequest("Customer not deleted");

        return Ok(result.Value);
    }


}