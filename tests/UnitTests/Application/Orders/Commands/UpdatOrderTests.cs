using Application.Common.Models;
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
            public async Task UpdateOrderAsync_ValidRequest_UpdatesOrderAndDetails()
            {
                // Arrange
                var orderEntity = new Order { Id = 1,OrderRef="erwerw" };
                var updateRequest = new UpdateOrderRequest { OrderId = 1 };
                var session = new UpdateOrderSession
                {
                    Order = updateRequest,
                    OrderDetails = new List<UpdateOrderDetailRequest> { new UpdateOrderDetailRequest() },
                    Customizations = new List<UpdateCustomizedOrderRequest> {new UpdateCustomizedOrderRequest() }
                    
                };

                _UnitOfWorkMock.SetupGet(u => u._orderRepository).Returns(_orderRepoMock.Object);
                _orderRepoMock.Setup(r => r.GetByIdAsync(updateRequest.OrderId)).ReturnsAsync(orderEntity);
                _orderRepoMock.Setup(r => r.Update(orderEntity));

                _mapperMock.Setup(m => m.ToUpdateEntity(orderEntity, updateRequest));

                _orderDetailCommandsMock
                    .Setup(d => d.UpdateOrderDetailAsync(It.IsAny<UpdateOrderDetailRequest>()))
                    .ReturnsAsync(Result<bool>.Success(true));

                _customizedOrderCommandsMock
                    .Setup(c => c.UpdateCustomizedOrderAsync(It.IsAny<UpdateCustomizedOrderRequest>()))
                   .ReturnsAsync(Result<bool>.Success(true));

            _UnitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

                // Act
                var result = await _orderCommands.UpdateOrderAsync(session);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
                _orderRepoMock.Verify(r => r.Update(orderEntity), Times.Once);
                _orderDetailCommandsMock.Verify(d => d.UpdateOrderDetailAsync(It.
                    IsAny<UpdateOrderDetailRequest>()), Times.Once);
                _customizedOrderCommandsMock.Verify(c => c.UpdateCustomizedOrderAsync(It.
                    IsAny<UpdateCustomizedOrderRequest>()), Times.Once);
                _UnitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            }

            [Fact]
            public async Task UpdateOrderAsync_NullRequest_ReturnsFailure()
            {
                // Act
                var result = await _orderCommands.UpdateOrderAsync(null);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Invalid update request.", result.Error);
            }

            [Fact]
            public async Task UpdateOrderAsync_OrderNotFound_ReturnsFailure()
            {
                // Arrange
                var session = new UpdateOrderSession
                {
                    Order = new UpdateOrderRequest { OrderId = 999 }
                };

                _UnitOfWorkMock.SetupGet(u => u._orderRepository).Returns(_orderRepoMock.Object);
                _orderRepoMock.Setup(r => r.GetByIdAsync(session.Order.OrderId)).ReturnsAsync((Order)null);

                // Act
                var result = await _orderCommands.UpdateOrderAsync(session);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Order Not Found", result.Error);
            }

            [Fact]
            public async Task UpdateOrderAsync_ThrowsException_ReturnsFailureWithMessage()
            {
                // Arrange
                var session = new UpdateOrderSession
                {
                    Order = new UpdateOrderRequest { OrderId = 1 }
                };

                _UnitOfWorkMock.SetupGet(u => u._orderRepository).Returns(_orderRepoMock.Object);
            _orderRepoMock.Setup(r => r.GetByIdAsync(session.Order.OrderId)).ThrowsAsync(new Exception("DB error"));

            // Act
            var result = await _orderCommands.UpdateOrderAsync(session);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("failed to update Order", result.Error);
        }
    }
}
