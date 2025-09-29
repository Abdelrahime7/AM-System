using Application.Customers.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Customers.Queries;

public partial class CustomerQueriesTests
{
    [Fact]
    public async Task GetByNameAsync_ShouldReturnSuccess_WhenCustomerIsFound()
    {
        //Arrange
        const string customerName = "john doe";
        var customer = new Customer
        {
            Id = 42,
            FullName = customerName,
            City = "Arizona",
            Address = "123 Wall Street, USA",
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

        _mockRepository.Setup(r => r.GetByNameAsync(customerName))
            .ReturnsAsync(customer);

        _mockMapper.Setup(m => m.ToResponse(customer))
            .Returns(response);

        //Act
        var result = await _queries.GetByNameAsync(customerName);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(customer.Id, result.Value.Id);
        Assert.Equal(customer.FullName, result.Value.FullName);

        _mockRepository.Verify(r => r.GetByNameAsync(customerName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(customer), Times.Once);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFailure_WhenCustomerNotFound()
    {
        //Arrange
        const string customerName = "ghost";
        _mockRepository.Setup(r => r.GetByNameAsync(customerName))
            .ReturnsAsync((Customer)null!);

        //Act
        var result = await _queries.GetByNameAsync(customerName);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Customer Found", result.Error);

        _mockRepository.Verify(r => r.GetByNameAsync(customerName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Customer>()), Times.Never);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const string customerName = "invalid";
        _mockRepository.Setup(r => r.GetByNameAsync(customerName))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByNameAsync(customerName);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch customer: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetByNameAsync(customerName), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Customer>()), Times.Never);
    }
}
