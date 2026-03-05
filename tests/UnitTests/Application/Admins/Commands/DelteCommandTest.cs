
using Domain.Entities;
using Moq;


namespace UnitTests.Application.Admins.Commands
{
    public partial class CommandTests
    {

       
           
            [Fact]
            public async Task DeleteAdminAsync_ReturnsSuccess_WhenAdminExists()
            {
                // Arrange
                var admin = new Admin { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
                _repository.Setup(r => r.Delete(admin));

                // Act
                var result = await _Commands.DeleteAdminAsnc(1);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
            }

            [Fact]
            public async Task DeleteAdminAsync_ReturnsFailure_WhenAdminNotFound()
            {
                // Arrange
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Admin)null);

                // Act
                var result = await _Commands.DeleteAdminAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Admin Not Found", result.Error);
            }

            [Fact]
            public async Task DeleteAdminAsync_ReturnsFailure_WhenRepositoryThrows()
            {
                // Arrange
                var admin = new Admin { Id = 1 };
                _repository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(admin);
                _repository.Setup(r => r.Delete(admin)).Throws(new Exception("DB error"));

                // Act
                var result = await _Commands.DeleteAdminAsnc(1);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("Error Deleting Admin", result.Error);
            }
        

    }
}
