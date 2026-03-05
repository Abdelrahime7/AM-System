using Application.CallsLog.DTOs;
using Application.CallsLog.Features.Commands;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.CalLogs.Commands
{
    public partial class CallLogCommandsTests
    {
      

        [Fact]
        public async Task UpdateCallLogAsync_ShouldReturnSuccess_WhenCallLogExists()
        {
            // Arrange
            var request = new UpdateCallLogRequest { Id = 1 };
            var existingCallLog = new CallLog { Id = 1,
                CustomerPhone="+213797759858" };

            _repository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(existingCallLog);
            _mapper.Setup(m => m.ToUpdateEntity(existingCallLog, request));
            _repository.Setup(r => r.Update(existingCallLog));

            var sut = CreateSut();

            // Act
            var result = await sut.UpdateCallLogAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task UpdateCallLogAsync_ShouldReturnFailure_WhenCallLogNotFound()
        {
            // Arrange
            var request = new UpdateCallLogRequest { Id = 99 };

            _repository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((CallLog)null);

            var sut = CreateSut();

            // Act
            var result = await sut.UpdateCallLogAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("call Log Not Found", result.Error);
        }

        [Fact]
        public async Task UpdateCallLogAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new UpdateCallLogRequest { Id = 2 };
            var callLog = new CallLog { Id = 2 ,
                CustomerPhone = "+213797759858"
            };

            _repository.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(callLog);
            _mapper.Setup(m => m.ToUpdateEntity(callLog, request)).Throws(new Exception("Mapping failed"));

            var sut = CreateSut();

            // Act
            var result = await sut.UpdateCallLogAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to update call Log", result.Error);
        }
    }

}
