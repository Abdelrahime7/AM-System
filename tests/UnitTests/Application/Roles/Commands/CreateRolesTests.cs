using Application.Roles.DTOs;
using Application.Roles.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Commands;

public partial class RoleCommandsTests
{
    private readonly Mock<IRoleRepository> _mockRepository;
    private readonly Mock<IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>> _mockMapper;
    private readonly RoleCommands _commands;
    
    public RoleCommandsTests()
    {
        _mockRepository = new Mock<IRoleRepository>();
        _mockMapper = new Mock<IEntityMapper<Role, CreateRoleRequest, UpdateRoleRequest, RoleResponse>>();
        _commands = new RoleCommands(_mockRepository.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task CreateRoleAsync_ShouldReturnSuccess_WhenRoleIsAdded()
    {
        //Arrange
        var request = new CreateRoleRequest
        {
            RoleType = UserRole.Admin
        };
        
        var newRole = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin
        };

        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType))
            .ReturnsAsync((Role)null!);
        _mockMapper.Setup(m => m.ToEntity(request)).Returns(newRole);
        _mockRepository.Setup(r => r.AddAsync(newRole));

        //Act
        var result = await _commands.CreateRoleAsync(request);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value);
        Assert.Equal(1, newRole.Id);
        Assert.Equal(request.RoleType, newRole.RoleType);
        _mockRepository.Verify(r => r.GetByRoleTypeAsync(request.RoleType), Times.Once);
        _mockMapper.Verify(m => m.ToEntity(request), Times.Once);
        _mockRepository.Verify(r => r.AddAsync(newRole), Times.Once);
    }

    [Fact]
    public async Task CreateRoleAsync_ShouldReturnFailure_WhenRoleTypeAlreadyExists()
    {
        // Arrange
        var request = new CreateRoleRequest
        {
            RoleType = UserRole.Admin
        };

        var existingRole = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin
        };

        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType))
            .ReturnsAsync(existingRole);

        // Act
        var result = await _commands.CreateRoleAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Role type '{request.RoleType}' already exists", result.Error);
        _mockRepository.Verify(r => r.GetByRoleTypeAsync(request.RoleType), Times.Once);
        _mockMapper.Verify(m => m.ToEntity(It.IsAny<CreateRoleRequest>()), Times.Never);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Role>()), Times.Never);
    }

    [Fact]
    public async Task CreateRoleAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new CreateRoleRequest
        {
            RoleType = UserRole.Affiliate
        };

        var role = new Role
        {
            Id = -1,
            RoleType = UserRole.Affiliate
        };

        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType))
            .ReturnsAsync((Role)null!);
        _mockMapper.Setup(m => m.ToEntity(It.IsAny<CreateRoleRequest>()))
            .Returns(role);
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Role>()))
            .ThrowsAsync(new Exception("DB Error"));
        
        // Act
        var result = await _commands.CreateRoleAsync(request);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error creating role: DB Error", result.Error);
    }
}
