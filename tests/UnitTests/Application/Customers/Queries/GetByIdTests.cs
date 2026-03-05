using Application.Customers.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Customers.Queries;

public partial class CustomerQueriesTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnSuccess_WhenCustomerIsFound()
    {
        //Arrange
        const int requestId = 32;
        var customer = new Customer
        {
            Id = 32,
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };
        
        var response = new CustomerResponse
        {
            Id = customer.Id,
            FullName = customer.FullName,
            City = customer.City,
            Address = customer.Address,
            Phone = customer.Phone
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync(customer);

        _mockMapper.Setup(m => m.ToResponse(customer))
            .Returns(response);

        // Act
        var result = await _queries.GetByIdAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(customer.Id, result.Value.Id);
        Assert.Equal(customer.FullName, result.Value.FullName);

        _mockRepository.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(customer), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenCustomerNotFound()
    {
        //Arrange
        const int requestId = 32;
        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync((Customer)null!);

        //Act
        var result = await _queries.GetByIdAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Customer Found", result.Error);
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByIdAsync(requestId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch customer: DB Error", result.Error);
    }
}
