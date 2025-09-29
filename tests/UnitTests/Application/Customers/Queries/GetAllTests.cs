using Application.Customers.DTOs;
using Application.Customers.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Customers.Queries;

public partial class CustomerQueriesTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>> _mockMapper;
    private readonly CustomerQueries _queries;
    
    public CustomerQueriesTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _mockMapper = new Mock<IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest, CustomerResponse>>();
        _queries = new CustomerQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllCustomerAsync_ShouldReturnSuccess_WhenCustomersAreFound()
    {
        // Arrange
        var customers = Enumerable.Range(1, 3).Select(i => new Customer
        {
            Id = i,
            FullName = $"Customer {i}",
            City = $"City {i}",
            Address = $"Street {i}",
            Phone = $"000{i}"
        }).ToList();

        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(customers);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<Customer>()))
                   .Returns<Customer>(c => new CustomerResponse
                   {
                       Id = c.Id,
                       FullName = c.FullName,
                       City = c.City,
                       Address = c.Address,
                       Phone = c.Phone
                   });

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(customers.Count, result.Value!.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Customer>()), Times.Exactly(customers.Count));
    }

    [Fact]
    public async Task GetAllCustomerAsync_ShouldReturnFailure_WhenCustomersNotFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<Customer>());

        // Act
        var result = await _queries.GetAllAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No Customers Found", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Customer>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllCustomerAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("failed to fetch customers: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
