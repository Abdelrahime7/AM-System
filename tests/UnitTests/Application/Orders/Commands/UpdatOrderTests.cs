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
        public async Task UpdateOrderAsync_ValidSession_ReturnsSuccess()
        {
            var session = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 42 },
                OrderDetails = [new UpdateOrderDetailRequest()],
                Customizations = [new UpdateCustomizedOrderRequest()]
            };

            var orderEntity = new Order { Id = 42 ,
                OrderRef="q3423423423"
            };

            _orderRepoMock.Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync(orderEntity);

            _orderRepoMock.Setup(r => r.Update(orderEntity));

            _orderDetailCommandsMock.Setup(d => d.UpdateOrderDetailAsync(It.IsAny<UpdateOrderDetailRequest>()))
                .ReturnsAsync(Result<bool>.Success(true));
            
            _customizedOrderCommandsMock.Setup(c => c.UpdateCustomizedOrderAsync(It.IsAny<UpdateCustomizedOrderRequest>()))
                .ReturnsAsync(Result<bool>.Success(true));

            _orderRepoMock.Setup(r => r.CommitAsync(default))
                .Returns(Task.CompletedTask);

            var result = await _orderCommands.UpdateOrderAsync(session);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
        [Fact]
        public async Task UpdateOrderAsync_NullSession_ReturnsFailure()
        {
            var result = await _orderCommands.UpdateOrderAsync(null);

            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid update request.", result.Error);
        }
        [Fact]
        public async Task UpdateOrderAsync_OrderNotFound_ReturnsFailure()
        {
            var session = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 999 }
            };

            _orderRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Order)null);

            var result = await _orderCommands.UpdateOrderAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Order Not Found", result.Error);
        }

        [Fact]
        public async Task UpdateOrderAsync_ThrowsException_ReturnsFailure()
        {
            var session = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 42 }
            };

            _orderRepoMock.Setup(r => r.GetByIdAsync(42))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _orderCommands.UpdateOrderAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Contains("failed to update Order", result.Error);
        }

    }
}
