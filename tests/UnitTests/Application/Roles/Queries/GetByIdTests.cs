using Application.Roles.DTOs;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Queries;

public partial class RoleQueriesTests
{
    [Fact]
    public async Task GetRoleByIdAsync_ShouldReturnSuccess_WhenRoleIsFound()
    {
        //Arrange
        const int requestId = 1;
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin,
            Users = new List<User> { new User
                {
                   Username="user123",
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
            UsersName = new List<string> { "John Doe" }
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync(role);

        _mockMapper.Setup(m => m.ToResponse(role))
            .Returns(response);

        // Act
        var result = await _queries.GetRoleByIdAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(role.Id, result.Value.Id);
        Assert.Equal(role.RoleType, result.Value.RoleType);
        Assert.Single(result.Value.UsersName);

        _mockRepository.Verify(r => r.GetByIdAsync(requestId), Times.Once);
        _mockMapper.Verify(m => m.ToResponse(role), Times.Once);
    }

    [Fact]
    public async Task GetRoleByIdAsync_ShouldReturnFailure_WhenRoleNotFound()
    {
        //Arrange
        const int requestId = 1;
        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ReturnsAsync((Role)null!);

        //Act
        var result = await _queries.GetRoleByIdAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Role not found", result.Error);
    }
    
    [Fact]
    public async Task GetRoleByIdAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId))
            .ThrowsAsync(new Exception("DB Error"));

        //Act
        var result = await _queries.GetRoleByIdAsync(requestId);

        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Failed to fetch role: DB Error", result.Error);
    }
}