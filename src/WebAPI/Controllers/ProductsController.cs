
using Application.Interfaces.ProductInterfaces;
using Application.Products.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(IProductQueries productQueries, IProductCommands productCommands)
    : ControllerBase
{
    private readonly IProductCommands _productCommands = productCommands;
    private readonly IProductQueries _productQueries = productQueries;
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: "ApprovedAffiliateOrSuperAdminOrAdmin")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        var result = await _productQueries.GetAllAsync();

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

    public async Task<ActionResult<ProductResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await _productQueries.GetByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("by-name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: "ApprovedAffiliateOrSuperAdminOrAdmin")]

    public async Task<ActionResult<ProductResponse>> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return BadRequest($"Invalid name = {name}");

        var result = await _productQueries.GetByNameAsync(name);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: "ApprovedAffiliateOrSuperAdminOrAdmin")]

    public async Task<ActionResult<int>> Create(CreateProductRequest request)
    {
      
         request.CreatedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _productCommands.CreateProductAsync(request);
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
    [Authorize(policy: "ApprovedAffiliateOrSuperAdminOrAdmin")]

    public async Task<ActionResult<ProductResponse>> Update(UpdateProductRequest request)
    {
        if (request.Id <= 0) 
            return BadRequest("Invalid input. Please check the submitted data");

        //Uncomment later when we add JWT
        // request.CreatedByUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _productCommands.UpdateProductAsync(request);
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
    [Authorize(policy:"ApprovedAdminOrsuperAdmin")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        if (id < 1)
            return NoContent();
        var result = await _productCommands.DeleteProductAsync(id);

        if (!result.IsSuccess)
            return BadRequest("Product not deleted");
            
        return Ok(result.Value);
    }
}