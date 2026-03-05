using Application.CallsLog.DTOs;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.CalLogs.Queries
{
    public partial class CallLogQueriesTests
    {
       
        [Fact]
        public async Task GetAllCallLogsAsync_ShouldReturnSuccess_WhenCallLogsExist()
        {
            // Arrange
            var callLogs = new List<CallLog>
        {
            new CallLog { Id = 1,
                CustomerPhone ="+213799445533" },
            new CallLog { Id = 2,
            CustomerPhone ="+213799445533"
            }
        };

            var responses = new List<CallLogrResponse>
        {
            new CallLogrResponse { /* fill with expected data */ },
            new CallLogrResponse { /* fill with expected data */ }
        };

            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(callLogs);
            _mapper.Setup(m => m.ToResponse(callLogs[0])).Returns(responses[0]);
            _mapper.Setup(m => m.ToResponse(callLogs[1])).Returns(responses[1]);

            var sut = CreateSut();

            // Act
            var result = await sut.GetAllCallLogsAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public async Task GetAllCallLogsAsync_ShouldReturnFailure_WhenNoCallLogsFound()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<CallLog>());

            var sut = CreateSut();

            // Act
            var result = await sut.GetAllCallLogsAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No callLogs Found", result.Error);
        }

        [Fact]
        public async Task GetAllCallLogsAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            _repository.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

            var sut = CreateSut();

            // Act
            var result = await sut.GetAllCallLogsAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch callLogs", result.Error);
        }
    }

}
