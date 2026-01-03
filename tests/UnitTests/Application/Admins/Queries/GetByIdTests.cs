

using Application.Admins.Dto_s;

using Application.Users.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Admins.Queries
{
    public partial class QueriesTest
    {
       

        [Fact]
        public async Task GetById_ReturnsSuccess_WhenAdminExists()
        {
            // Arrange
            var admin = new Admin { Id = 1, user = new User {
                Role = Domain.Enums.UserRole.Admin,

                Id = 10,
                FullName= "john smith",
                Phone="+213566443322",
                Username="werw34w3",
                PasswordHash="tewtr3qrwr"
            } };
            var adminResponse = new AdminResponse { levels= (Domain.Enums.AccessLevels )1};
            var userResponse = new UserResponse { Id = 10 };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _mapper.Setup(m => m.ToResponse(admin)).Returns(adminResponse);
            _userMapper.Setup(u => u.ToResponse(admin.user)).Returns(userResponse);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(10, result.Value.UserResponse.Id);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenAdminNotFound()
        {
            // Arrange
            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Admin)null);

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No Admin Found", result.Error);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenRepositoryThrows()
        {
            // Arrange
            _repository.Setup(r => r.GetByIdAsync(1)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch Admin", result.Error);
        }

        [Fact]
        public async Task GetById_ReturnsFailure_WhenMapperThrows()
        {
            // Arrange
            var admin = new Admin { Id = 1, user = new User { Id = 10,
                Role = Domain.Enums.UserRole.Admin,

                FullName = "john smith",
             PasswordHash="34234242w",
             Phone="+2135667788",
             Username ="wddee rr3w"}
             };

            _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
            _mapper.Setup(m => m.ToResponse(admin)).Throws(new Exception("Mapping error"));

            // Act
            var result = await _queriesMock.Object.GetById(1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch Admin", result.Error);
        }
    }

}
