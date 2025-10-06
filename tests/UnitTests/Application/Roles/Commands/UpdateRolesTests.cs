using Application.Roles.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Commands;

public partial class RoleCommandsTests
{
    [Fact]
    public async Task UpdateRoleAsync_ShouldReturnSuccess_WhenRoleIsUpdated()
    {
        //Arrange
        var request = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = UserRole.Admin
        };
        
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Affiliate
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(role);
        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType!.Value))
            .ReturnsAsync((Role)null!);
        
        // Act
        var result = await _commands.UpdateRoleAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockMapper.Verify(m => m.ToUpdateEntity(role, request), Times.Once);
        _mockRepository.Verify(r => r.Update(role), Times.Once);
    }

    [Fact]
    public async Task UpdateRoleAsync_ShouldReturnSuccess_WhenRoleTypeNotChanged()
    {
        //Arrange
        var request = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = null // Not changing role type
        };
        
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(role);
        
        // Act
        var result = await _commands.UpdateRoleAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockRepository.Verify(r => r.GetByRoleTypeAsync(It.IsAny<UserRole>()), Times.Never);
        _mockMapper.Verify(m => m.ToUpdateEntity(role, request), Times.Once);
        _mockRepository.Verify(r => r.Update(role), Times.Once);
    }

    [Fact]
    public async Task UpdateRoleAsync_ShouldReturnFailure_WhenRoleNotFound()
    {
        //Arrange
        var request = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = UserRole.Admin
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Role)null!);

        //Act
        var result = await _commands.UpdateRoleAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Role not found", result.Error);
    }
    
    [Fact]
    public async Task UpdateRoleAsync_ShouldReturnFailure_WhenRoleTypeAlreadyExists()
    {
        //Arrange
        var request = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = UserRole.Admin
        };
        
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Affiliate
        };

        var existingRole = new Role
        {
            Id = 2,
            RoleType = UserRole.Admin
        };

        _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
            .ReturnsAsync(role);
        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType!.Value))
            .ReturnsAsync(existingRole);

        //Act
        var result = await _commands.UpdateRoleAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Role type '{request.RoleType}' already exists", result.Error);
        _mockMapper.Verify(m => m.ToUpdateEntity(It.IsAny<Role>(), It.IsAny<UpdateRoleRequest>()), Times.Never);
        _mockRepository.Verify(r => r.Update(It.IsAny<Role>()), Times.Never);
    }

    [Fact]
    public async Task UpdateRoleAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var request = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = UserRole.Admin
        };
        
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Affiliate
        };

        _mockRepository.Setup(m => m.GetByIdAsync(request.Id)).ReturnsAsync(role);
        _mockRepository.Setup(r => r.GetByRoleTypeAsync(request.RoleType!.Value))
            .ReturnsAsync((Role)null!);
        _mockRepository.Setup(r => r.Update(It.IsAny<Role>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.UpdateRoleAsync(request);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to update role: DB Error", result.Error);
    }
}