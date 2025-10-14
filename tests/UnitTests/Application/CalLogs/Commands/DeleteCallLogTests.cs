
using Domain.Entities;
using Moq;

namespace UnitTests.Application.CalLogs.Commands
{
    public partial class CallLogCommandsTests
    {


        [Fact]
        public async Task DeleteCallLogAsync_ShouldReturnSuccess_WhenCallLogExists()
        {
            // Arrange
            var id = 1;
            var callLog = new CallLog { Id = id,
                CustomerPhone = "+213766005588"
            };

            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(callLog);
            _repository.Setup(r => r.Delete(callLog));

            var sut = CreateSut();

            // Act
            var result = await sut.DeleteCallLogAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task DeleteCallLogAsync_ShouldReturnFailure_WhenCallLogNotFound()
        {
            // Arrange
            var id = 99;

            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((CallLog)null);

            var sut = CreateSut();

            // Act
            var result = await sut.DeleteCallLogAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("call Log Not Found", result.Error);
        }

        [Fact]
        public async Task DeleteCallLogAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 2;
            var callLog = new CallLog { Id = id,
                CustomerPhone = "+213766005588"
            };

            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(callLog);
            _repository.Setup(r => r.Delete(callLog)).Throws(new Exception("Delete failed"));

            var sut = CreateSut();

            // Act
            var result = await sut.DeleteCallLogAsync(id);
    }
    }

}