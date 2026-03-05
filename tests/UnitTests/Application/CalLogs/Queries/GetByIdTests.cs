using Application.CallsLog.DTOs;
using Application.CallsLog.Features.Queries;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.CalLogs.Queries
{
    public partial class CallLogQueriesTests
    {
        private readonly Mock<ICallLogRepository> _repository = new();
        private readonly Mock<IEntityMapper<CallLog, CreateCallLogRequest, UpdateCallLogRequest, CallLogrResponse>> _mapper = new();

        private CallLogQueries CreateSut() =>
            new(_repository.Object, _mapper.Object);

        [Fact]
        public async Task GetCallLogByIDAsync_ShouldReturnSuccess_WhenCallLogExists()
        {
            // Arrange
            var id = 1;
            var callLog = new CallLog { Id = id,
                CustomerPhone="+213793246467"
            };
            var response = new CallLogrResponse { /* fill with expected response data */ };

            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(callLog);
            _mapper.Setup(m => m.ToResponse(callLog)).Returns(response);

            var sut = CreateSut();

            // Act
            var result = await sut.GetCallLogByIDAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(response, result.Value);
        }

        [Fact]
        public async Task GetCallLogByIDAsync_ShouldReturnFailure_WhenCallLogNotFound()
        {
            // Arrange
            var id = 99;

            _repository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((CallLog)null);

            var sut = CreateSut();

            // Act
            var result = await sut.GetCallLogByIDAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No call Log Found", result.Error);
        }

        [Fact]
        public async Task GetCallLogByIDAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 2;

            _repository.Setup(r => r.GetByIdAsync(id)).ThrowsAsync(new Exception("DB failure"));

            var sut = CreateSut();

            // Act
            var result = await sut.GetCallLogByIDAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to fetch call Log", result.Error);
        }
    }

}
