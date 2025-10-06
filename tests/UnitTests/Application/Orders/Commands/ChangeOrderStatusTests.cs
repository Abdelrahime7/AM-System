using Application.Orders.DTOs;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.Orders.Commands
{
    public partial  class OrderCommandsTests
    {
        [Fact]
        public async Task ChangeOrderStatusAsync_ValidRequest_ReturnsSuccess()
        {
            var request = new UpdateOrderRequest { OrderId = 42 };
            var order = new Order { Id = 42,OrderRef="wrewrwqrqwq" };

            _orderRepoMock.Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync(order);

            _orderRepoMock.Setup(r => r.Update(order));

            var result = await _orderCommands.ChangeOrderStatusAsync(request);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            _orderRepoMock.Verify(r => r.Update(order), Times.Once);
        }
        [Fact]
        public async Task ChangeOrderStatusAsync_OrderNotFound_ReturnsFailure()
        {
            var request = new UpdateOrderRequest { OrderId = 999 };

            _orderRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Order)null);

            var result = await _orderCommands.ChangeOrderStatusAsync(request);

            Assert.False(result.IsSuccess);
            Assert.Equal("No order found", result.Error);
            _orderRepoMock.Verify(r => r.Update(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task ChangeOrderStatusAsync_ThrowsException_ReturnsFailure()
        {
            var request = new UpdateOrderRequest { OrderId = 1 };

            _orderRepoMock.Setup(r => r.GetByIdAsync(1))
                .ThrowsAsync(new Exception("DB failure"));

            var result = await _orderCommands.ChangeOrderStatusAsync(request);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to change order status", result.Error);
        }

    }
}
