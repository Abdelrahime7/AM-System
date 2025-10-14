using Application.Orders.DTOs;
using Domain.Entities;
using Domain.Enums;
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
            public async Task ChangeOrderStatusAsync_OrderExists_UpdatesStatusSuccessfully()
            {
                // Arrange
                var request = new ChangeOrderStatus { Id = 1, Status = OrderStatus.Delivered };
                var order = new Order { Id = 1, Status = OrderStatus.Approved,OrderRef="32312qwe" };

                _UnitOfWorkMock.SetupGet(u => u._orderRepository).Returns(_orderRepoMock.Object);
                _orderRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(order);
                _orderRepoMock.Setup(r => r.Update(order));

                // Act
                var result = await _orderCommands.ChangeOrderStatusAsync(request);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
                Assert.Equal(OrderStatus.Delivered, order.Status);
                _orderRepoMock.Verify(r => r.Update(order), Times.Once);
            }

            [Fact]
            public async Task ChangeOrderStatusAsync_OrderNotFound_ReturnsFailure()
            {
                // Arrange
                var request = new ChangeOrderStatus { Id = 999, Status = OrderStatus.Approved };

                _UnitOfWorkMock.SetupGet(u => u._orderRepository).Returns(_orderRepoMock.Object);
                _orderRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync((Order)null);

                // Act
                var result = await _orderCommands.ChangeOrderStatusAsync(request);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("No order found", result.Error);
                _orderRepoMock.Verify(r => r.Update(It.IsAny<Order>()), Times.Never);
            }

          
                [Fact]
        public async Task ChangeOrderStatusAsync_RepositoryThrowsException_ReturnsFailure()
        {
            // Arrange
            var request = new ChangeOrderStatus { Id = 1, Status = OrderStatus.Delivered };
            _orderRepoMock.Setup(r => r.GetByIdAsync(request.Id)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _orderCommands.ChangeOrderStatusAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to change order status", result.Error);
        }
    }

}
