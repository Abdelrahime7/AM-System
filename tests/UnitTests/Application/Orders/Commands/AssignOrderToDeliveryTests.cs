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
        public async Task AssignOrderToDelivery_CityIsAlgiers_UsesLocalStrategy()
        {
            var order = new Order
            {
                OrderRef="rrwerwRWda233",
                Customer = new Customer { City = "Algiers" ,
                FullName="weqweqwerwrqwq",
                Phone ="+213-544332266"}
            };

            _Local.Setup(l => l.AssignAsync(order))
                .Returns(Task.CompletedTask);

            var result = await _orderCommands.AssignOrderToDelivery(order);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

            _Local.Verify(l => l.AssignAsync(order), Times.Once);
            _External.Verify(e => e.AssignAsync(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task AssignOrderToDelivery_CityIsNotAlgiers_UsesExternalStrategy()
        {
            var order = new Order
            {
                OrderRef="321312412421",
                Customer = new Customer { City = "Oran",
                    FullName="john smith",
                 Phone="+213-544332211"}
            };

            _External.Setup(e => e.AssignAsync(order))
                .Returns(Task.CompletedTask);

            var result = await _orderCommands.AssignOrderToDelivery(order);

            Assert.True(result.IsSuccess);
            Assert.True(result.Value);

            _External.Verify(e => e.AssignAsync(order), Times.Once);
            _Local.Verify(l => l.AssignAsync(It.IsAny<Order>()), Times.Never);
        }

        [Fact]
        public async Task AssignOrderToDelivery_ThrowsException_ReturnsFailure()
        {
            var order = new Order
            {
                OrderRef = "rrwerwRWda233",
                Customer = new Customer
                {
                    City = "Algiers",
                    FullName = "weqweqwerwrqwq",
                    Phone = "+213-544332266"
                }
            };

            _Local.Setup(l => l.AssignAsync(order))
                .ThrowsAsync(new Exception("Network error"));

            var result = await _orderCommands.AssignOrderToDelivery(order);

            Assert.False(result.IsSuccess);
            Assert.Contains("failed to Assign Order", result.Error);
        }

    }

}