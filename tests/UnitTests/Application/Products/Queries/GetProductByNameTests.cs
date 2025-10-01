using Application.Products.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Products.Queries;

public partial class ProductQueriesTests
{
    [Fact]
    public async Task GetByNameAsync_ShouldReturnSuccess_WhenProductIsFound()
    {
        //Arrange
        const string productName = "Test Product";
        var product = new Product { Id = 1, Name = productName };
        var productResponse = new AffiliateBalanceResponse { Id = 1, Name = productName };

        _mockRepository.Setup(r => r.GetByNameAsync(productName)).ReturnsAsync(product);
        _mockMapper.Setup(m => m.ToResponse(product)).Returns(productResponse);

        //Act
        var result = await _queries.GetByNameAsync(productName);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(productName, result.Value.Name);
        _mockRepository.Verify(r => r.GetByNameAsync(productName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(product), Times.Once);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFailure_WhenProductNotFound()
    {
        //Arrange
        const string productName = "Ghost Product";
        _mockRepository.Setup(r => r.GetByNameAsync(productName)).ReturnsAsync((Product)null!);

        //Act
        var result = await _queries.GetByNameAsync(productName);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Product Found", result.Error);
        _mockRepository.Verify(r => r.GetByNameAsync(productName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const string productName = "Error Product";
        _mockRepository.Setup(r => r.GetByNameAsync(productName)).ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByNameAsync(productName);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch product: DB Error", result.Error);
        _mockRepository.Verify(r => r.GetByNameAsync(productName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Product>()), Times.Never);
    }
}
