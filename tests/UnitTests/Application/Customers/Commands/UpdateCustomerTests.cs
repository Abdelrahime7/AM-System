using Application.Customers.DTOs;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace UnitTests.Application.Customers.Commands;

public partial class CustomerCommandsTests
{
    [Fact]
    public async Task UpdateCustomerAsync_ShouldReturnSuccess_WhenCustomerIsUpdated()
    {
        //Arrange
        var request = new UpdateCustomerRequest
        {
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };
        
        var customer = new Customer
        {
            Id = 32,
            FullName = "Hello World",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(customer);
        
        // Act
        var result = await _commands.UpdateCustomerAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(request.FullName, customer.FullName);
        Assert.Equal(request.Address, customer.Address);
        Assert.Equal(request.Phone, customer.Phone);
        _mockRepository.Verify(r => r.Update(customer), Times.Once);
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldReturnFailure_WhenCustomerNotFound()
    {
        //Arrange
        var request = new UpdateCustomerRequest
        {
            Id = 32,
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Customer)null!);

        //Act
        var result = await _commands.UpdateCustomerAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Customer Not Found", result.Error);
    }
    
    [Fact]
    public async Task UpdateCustomerAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateCustomerRequest
        {
            Id = 2,
            FullName = "",
            City = "",
            Address = "",
            Phone = ""
        };
        
        var customer = new Customer
        {
            Id = -1,
            FullName = "xyz",
            City = "xyz",
            Address = "xyz",
            Phone = "xyz"
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(customer);
        _mockRepository.Setup(r => r.Update(It.IsAny<Customer>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateCustomerAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to update customer: DB Error", result.Error);
    }
}