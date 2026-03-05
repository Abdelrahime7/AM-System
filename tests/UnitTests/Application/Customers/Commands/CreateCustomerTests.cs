using Application.Customers.DTOs;
using Application.Customers.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Customers.Commands;

public partial class CustomerCommandsTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>> _mockMapper;
    private readonly CustomerCommands _commands;
    
    public CustomerCommandsTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _mockMapper = new Mock<IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>>();
        _commands = new CustomerCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateCustomerAsync_ShouldReturnSuccess_WhenCustomerIsAdded()
    {
        //Arrange
        var request = new CreateCustomerRequest
        {
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };
        
        var existingCustomer = new Customer
        {
            Id = 32,
            FullName = "john doe",
            City = "Arizona",
            Address = "Arizona 123 wall street N3, USA",
            Phone = "0540112233"
        };

        _mockMapper.Setup(m => m.ToEntity(request)).Returns(existingCustomer);
        _mockRepository.Setup(r => r.AddAsync(existingCustomer));

        //Act
        var result = await _commands.CreateCustomerAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(32, existingCustomer.Id);
        Assert.Equal(request.FullName, existingCustomer.FullName);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(existingCustomer), Times.Once);
    }

    [Fact]
    public async Task CreateCustomerAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateCustomerRequest
        {
            FullName = "",
            City = "",
            Address = "",
            Phone = ""
        };

        var customer = new Customer
        {
            Id = -1,
            FullName = "",
            City = "",
            Address = "",
            Phone = ""
        };

        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateCustomerRequest>()))
            .Returns(customer);

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Customer>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateCustomerAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating customer: DB Error", result.Error);
    }
}