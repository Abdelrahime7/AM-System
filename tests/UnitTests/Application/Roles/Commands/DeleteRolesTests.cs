using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.Roles.Commands;

public partial class RoleCommandsTests
{
    [Fact]
    public async Task DeleteRoleAsync_ShouldReturnSuccess_WhenRoleIsDeleted()
    {
        //Arrange
        const int requestId = 1;
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin,
            Users = new List<User>()
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(role);
        
        // Act
        var result = await _commands.DeleteRoleAsync(requestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        _mockRepository.Verify(r => r.Delete(role), Times.Once);
    }

    [Fact]
    public async Task DeleteRoleAsync_ShouldReturnFailure_WhenRoleNotFound()
    {
        //Arrange
        const int requestId = -1;

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync((Role)null!);

        //Act
        var result = await _commands.DeleteRoleAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Role not found", result.Error);
    }
    
    [Fact]
    public async Task DeleteRoleAsync_ShouldReturnFailure_WhenRoleHasUsers()
    {
        //Arrange
        const int requestId = 1;
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin,
            Users = new List<User> { new User
                {
                    Id = 1,
                    FullName = "John Doe",
                    Email = null!,
                    Phone = null!,
                    PasswordHash = null!
                }
            }
        };

        _mockRepository.Setup(r => r.GetByIdAsync(requestId)).ReturnsAsync(role);

        //Act
        var result = await _commands.DeleteRoleAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Cannot delete role that has users assigned", result.Error);
        _mockRepository.Verify(r => r.Delete(It.IsAny<Role>()), Times.Never);
    }

    [Fact]
    public async Task DeleteRoleAsync_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        //Arrange
        const int requestId = 1;
        var role = new Role
        {
            Id = 1,
            RoleType = UserRole.Admin,
            Users = new List<User>()
        };

        _mockRepository.Setup(m => m.GetByIdAsync(requestId)).ReturnsAsync(role);
        _mockRepository.Setup(r => r.Delete(It.IsAny<Role>())).Throws(new Exception("DB Error"));

        //Act
        var result = await _commands.DeleteRoleAsync(requestId);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error deleting role: DB Error", result.Error);
    }
}