using Application.Customers.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Customers.Commands;

public partial class CustomerCommandsTests
{
    [Fact]
    public async Task DeleteCustomerAsync_ShouldReturnSuccess_WhenCustomerIsDeleted()
    {
        //Arrange
        const int requestId = 32;
        var customer = new Customer
        {
            Id = 32,
            FullName = "Hello World",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(customer);
        
        // Act
        var result = await _commands.DeleteCustomerAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        _mockRepository.Verify(r => r.Delete(customer), Times.Once);
    }

    [Fact]
    public async Task DeleteCustomerAsync_ShouldReturnFailure_WhenCustomerNotFound()
    {
        //Arrange
        const int requestId = -1;
        var customer = new Customer
        {
            Id = 32,
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync((Customer)null!);

        //Act
        var result = await _commands.DeleteCustomerAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Customer Not Found", result.Error);
    }
    
    [Fact]
    public async Task DeleteCustomerAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = -1;
        var customer = new Customer
        {
            Id = -1,
            FullName = "",
            City = "",
            Address = "",
            Phone = ""
        };

        _mockRepository.Setup(m => m.GetByIdAsync(requestId)).ReturnsAsync(customer);
        _mockRepository.Setup(r => r.Delete(It.IsAny<Customer>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.DeleteCustomerAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to delete customer: DB Error", result.Error);
    }
}