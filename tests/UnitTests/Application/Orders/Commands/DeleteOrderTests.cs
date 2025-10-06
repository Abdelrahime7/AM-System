using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.Orders.Commands
{
    public partial class OrderCommandsTests
    {
        [Fact]
        public async Task DeleteOrderAsync_ValidId_ReturnsSuccess()
        {
            var order = new Order { Id = 42 ,OrderRef="e2233432423"};

            _orderRepoMock.Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync(order);

            _orderRepoMock.Setup(r => r.Delete(order));

            var result = await _orderCommands.DeleteOrderAsync(42);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
        [Fact]
        public async Task DeleteOrderAsync_OrderNotFound_ReturnsFailure()
        {
            _orderRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Order)null);

            var result = await _orderCommands.DeleteOrderAsync(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Order Not Found", result.Error);


        }
        [Fact]
        public async Task DeleteOrderAsync_ThrowsException_ReturnsFailure()
        {
            _orderRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database failure"));

            var result = await _orderCommands.DeleteOrderAsync(1);

            Assert.False(result.IsSuccess);
            Assert.Contains("failed to delete Order", result.Error);
        }


    }
}
