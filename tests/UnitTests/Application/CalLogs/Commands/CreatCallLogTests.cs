using Application.CallsLog.DTOs;
using Application.CallsLog.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace UnitTests.Application.CalLogs.Commands
{
    public partial class CallLogCommandsTests
    {
        private readonly Mock<ICallLogRepository> _repository = new();
        private readonly Mock<IEntityMapper<CallLog
            , CreateCallLogRequest, UpdateCallLogRequest, CallLogrResponse>> _mapper = new();

        private CallLogCommands CreateSut() =>
            new(_repository.Object, _mapper.Object);

        [Fact]
        public async Task CreatCallLogAsync_ShouldReturnSuccess_WhenCallLogIsAdded()
        {
            // Arrange
            var request = new CreateCallLogRequest
            {
                CustomerPhone = "+213709994422",
                CallResult = CallResult.Confirmed,
                AgentId = 4,
                OrderId = 21
            };
            var callLog = new CallLog { Id = 1 ,
                CustomerPhone = "+213709994422"
            };

            _mapper.Setup(m => m.ToEntity(request)).Returns(callLog);
            _repository.Setup(r => r.AddAsync(callLog)).Returns(Task.CompletedTask);

            var sut = CreateSut();

            // Act
            var result = await sut.CreatCallLogAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task CreatCallLogAsync_ShouldReturnFailure_WhenRepositoryThrows()
        {
            // Arrange
            var request = new CreateCallLogRequest
            {
                CustomerPhone = "+213709994422",
                CallResult = CallResult.Confirmed,
                AgentId = 4,
                OrderId = 21
            };
            var callLog = new CallLog
            {
                Id = 1,
                CustomerPhone = "+213709994422"
            };


            _mapper.Setup(m => m.ToEntity(request)).Returns(callLog);
            _repository.Setup(r => r.AddAsync(callLog)).ThrowsAsync(new Exception("DB error"));

            var sut = CreateSut();

            // Act
            var result = await sut.CreatCallLogAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error creating callLog", result.Error);
        }
    }

}
