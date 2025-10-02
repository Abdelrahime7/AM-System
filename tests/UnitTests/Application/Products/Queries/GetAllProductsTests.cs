using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using Application.Products.Features.Queries;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Products.Queries;

public partial class ProductQueriesTests
{
    private readonly Mock<IProductRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse>> _mockMapper;
    private readonly ProductQueries _queries;
    
    public ProductQueriesTests()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockMapper = new Mock<IEntityMapper<Product, CreateProductRequest, UpdateProductRequest, ProductResponse>>();
        _queries = new ProductQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnSuccess_WhenProductsAreFound()
    {
        // Arrange
        var products = new List<Product> 
        { 
            new Product { Id = 1, Name = "Product 1" }, 
            new Product { Id = 2, Name = "Product 2" } 
        };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
        _mockMapper.Setup(m => m.ToResponse(It.IsAny<Product>()))
                   .Returns<Product>(p => new ProductResponse { Id = p.Id, Name = p.Name });

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(2, result.Value.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Product>()), Times.Exactly(2));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenNoProductsFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

        // Act
        var result = await _queries.GetAllAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Products Found", result.Error);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Product>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch products: DB Error", result.Error);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
