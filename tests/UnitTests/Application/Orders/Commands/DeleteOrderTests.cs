using Application.Common.Models;
using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
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
            public async Task DeleteOrderAsync_OrderExists_ReturnsSuccess()
            {
                // Arrange
                var order = new Order { Id = 1 ,OrderRef="r2342"};

                _orderRepoMock
                    .Setup(r => r.GetByIdAsync(order.Id))
                    .ReturnsAsync(order);

                _orderRepoMock
                    .Setup(r => r.Delete(order));

                _UnitOfWorkMock
                    .SetupGet(u => u._orderRepository)
                    .Returns(_orderRepoMock.Object);

                // Act
                var result = await _orderCommands.DeleteOrderAsync(order.Id);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
                _orderRepoMock.Verify(r => r.Delete(order), Times.Once);
            }

            [Fact]
            public async Task DeleteOrderAsync_OrderNotFound_ReturnsFailure()
            {
                // Arrange
                int orderId = 999;

                _orderRepoMock
                    .Setup(r => r.GetByIdAsync(orderId))
                    .ReturnsAsync((Order)null);

                _UnitOfWorkMock
                    .SetupGet(u => u._orderRepository)
                    .Returns(_orderRepoMock.Object);

                // Act
                var result = await _orderCommands.DeleteOrderAsync(orderId);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Order Not Found", result.Error);
                _orderRepoMock.Verify(r => r.Delete(It.IsAny<Order>()), Times.Never);
            }

            [Fact]
            public async Task DeleteOrderAsync_RepositoryThrowsException_ReturnsFailureWithMessage()
            {
                // Arrange
                int orderId = 1;

                _orderRepoMock
                    .Setup(r => r.GetByIdAsync(orderId))
                    .ThrowsAsync(new Exception("DB error"));

                _UnitOfWorkMock
                    .SetupGet(u => u._orderRepository)
                    .Returns(_orderRepoMock.Object);

                // Act
                var result = await _orderCommands.DeleteOrderAsync(orderId);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("failed to delete Order", result.Error);
            }
        }



    
}
