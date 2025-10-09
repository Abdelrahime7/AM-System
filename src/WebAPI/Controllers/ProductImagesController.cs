using Application.Interfaces.ProductImagesInterfaces;
using Application.ProductImages.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/product-images")]
public class ProductImagesController(IProductImageQueries productImageQueries, IProductImageCommands productImageCommands)
    : ControllerBase
{
    private readonly IProductImageCommands _productImageCommands = productImageCommands;
    private readonly IProductImageQueries _productImageQueries = productImageQueries;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<ProductImageResponse>>> GetAll()
    {
        var result = await _productImageQueries.GetAllAsync();
        if (result.IsSuccess)
            return Ok(result.Value);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductImageResponse>> GetById(int id)
    {
        if (id < 1)
            return BadRequest($"Invalid id = {id}");

        var result = await _productImageQueries.GetByIdAsync(id);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("product/{productId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductImageResponse>>> GetByProductId(int productId)
    {
        if (productId < 1)
            return BadRequest($"Invalid product id = {productId}");

        var result = await _productImageQueries.GetByProductIdAsync(productId);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("customized-order/{customizedOrderId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductImageResponse>>> GetByCustomizedOrderId(int customizedOrderId)
    {
        if (customizedOrderId < 1)
            return BadRequest($"Invalid customized order id = {customizedOrderId}");

        var result = await _productImageQueries.GetByCustomizedOrderIdAsync(customizedOrderId);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("product/{productId:int}/primary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductImageResponse>> GetPrimaryImageByProductId(int productId)
    {
        if (productId < 1)
            return BadRequest($"Invalid product id = {productId}");

        var result = await _productImageQueries.GetPrimaryImageByProductIdAsync(productId);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpGet("customized-order/{customizedOrderId:int}/primary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductImageResponse>> GetPrimaryImageByCustomizedOrderId(int customizedOrderId)
    {
        if (customizedOrderId < 1)
            return BadRequest($"Invalid customized order id = {customizedOrderId}");

        var result = await _productImageQueries.GetPrimaryImageByCustomizedOrderIdAsync(customizedOrderId);
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(CreateProductImageRequest request)
    {
        var result = await _productImageCommands.CreateProductImageAsync(request);
        if (result.IsSuccess)
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, request);

        return BadRequest(result.Error);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(UpdateProductImageRequest request)
    {
        if (request.Id <= 0)
            return BadRequest("Invalid input. Please check the submitted data");

        var result = await _productImageCommands.UpdateProductImageAsync(request);
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
            return BadRequest("Invalid product image ID");

        var result = await _productImageCommands.DeleteProductImageAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}