using Application.Roles.DTOs;
using Application.Roles.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Queries;

public partial class RoleQueriesTests
{
    private readonly Mock<IRoleRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>> _mockMapper;
    private readonly RoleQueries _queries;
    
    public RoleQueriesTests()
    {
        _mockRepository = new Mock<IRoleRepository>();
        _mockMapper = new Mock<IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>>();
        _queries = new RoleQueries(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllRolesAsync_ShouldReturnSuccess_WhenRolesAreFound()
    {
        // Arrange
        var roles = new List<Role>
        {
            new() { Id = 1, RoleType = UserRole.Admin },
            new() { Id = 2, RoleType = UserRole.Affiliate },
            new() { Id = 3, RoleType = UserRole.Driver }
        };

        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(roles);

        _mockMapper.Setup(m => m.ToResponse(It.IsAny<Role>()))
                   .Returns<Role>(r => new RoleResponse
                   {
                       Id = r.Id,
                       RoleType = r.RoleType,
                       UsersName = new List<string>()
                   });

        // Act
        var result = await _queries.GetAllRolesAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(roles.Count, result.Value!.Count());
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Role>()), Times.Exactly(roles.Count));
    }

    [Fact]
    public async Task GetAllRolesAsync_ShouldReturnFailure_WhenRolesNotFound()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<Role>());

        // Act
        var result = await _queries.GetAllRolesAsync();
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("No roles found", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Role>()), Times.Never);
    }
    
    [Fact]
    public async Task GetAllRolesAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAllAsync())
                       .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await _queries.GetAllRolesAsync();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to fetch roles: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}


