using Application.Roles.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Queries;

public partial class RoleQueriesTests
{
    [Fact]
    public async Task GetByRoleTypeAsync_ShouldReturnSuccess_WhenRoleIsFound()
    {
        //Arrange
        var roleType = UserRole.Admin;
        var role = new Role
        {
            Id = 1,
            RoleType = roleType,
            Users = new List<User> { new User
                {
                    Username = "Admin User",
                           Email = "old@example.com",
                           FullName = "john doe",
                         Phone = "0611223344",

                    PasswordHash = null!
                }
            }
        };
        
        var response = new RoleResponse
        {
            Id = role.Id,
            RoleType = role.RoleType,
            UsersName = new List<string> { "Admin User" }
        };

        _mockRepository.Setup(r => r.GetByRoleTypeAsync(roleType))
            .ReturnsAsync(role);

        _mockMapper.Setup(m => m.ToResponse(role))
            .Returns(response);

        //Act
        var result = await _queries.GetByRoleTypeAsync(roleType);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(role.Id, result.Value.Id);
        Assert.Equal(role.RoleType, result.Value.RoleType);
        Assert.Single(result.Value.UsersName);

        _mockRepository.Verify(r => r.GetByRoleTypeAsync(roleType), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(role), Times.Once);
    }

    [Fact]
    public async Task GetByRoleTypeAsync_ShouldReturnFailure_WhenRoleNotFound()
    {
        //Arrange
        var roleType = UserRole.CallCenterAgent;
        _mockRepository.Setup(r => r.GetByRoleTypeAsync(roleType))
            .ReturnsAsync((Role)null!);

        //Act
        var result = await _queries.GetByRoleTypeAsync(roleType);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Role type '{roleType}' not found", result.Error);

        _mockRepository.Verify(r => r.GetByRoleTypeAsync(roleType), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Role>()), Times.Never);
    }

    [Fact]
    public async Task GetByRoleTypeAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        var roleType = UserRole.Driver;
        _mockRepository.Setup(r => r.GetByRoleTypeAsync(roleType))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetByRoleTypeAsync(roleType);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to fetch role: DB Error", result.Error);

        _mockRepository.Verify(r => r.GetByRoleTypeAsync(roleType), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(It.IsAny<Role>()), Times.Never);
    }
}