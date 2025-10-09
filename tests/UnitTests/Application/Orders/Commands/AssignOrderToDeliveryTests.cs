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
                // Arrange
                var order = new Order
                {
                    OrderRef="qeqweq",
                    Customer = new Customer { City = "Algiers" ,
                    FullName="john smith",
                    Phone="+213758049833"
                    }
                };

                _Local.Setup(l => l.AssignAsync(order)).Returns(Task.CompletedTask);

                // Act
                var result = await _orderCommands.AssignOrderToDelivery(order);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
                _Local.Verify(l => l.AssignAsync(order), Times.Once);
                _External.Verify(e => e.AssignAsync(It.IsAny<Order>()), Times.Never);
            }

            [Fact]
            public async Task AssignOrderToDelivery_CityIsNotAlgiers_UsesExternalStrategy()
            {
            // Arrange
            var order = new Order
            {
                OrderRef = "qeqweq",
                Customer = new Customer
                {
                    City = "Oran",
                    FullName = "john smith",
                    Phone = "+213758049833"
                }
            };

            _External.Setup(e => e.AssignAsync(order)).Returns(Task.CompletedTask);

                // Act
                var result = await _orderCommands.AssignOrderToDelivery(order);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.True(result.Value);
                _External.Verify(e => e.AssignAsync(order), Times.Once);
                _Local.Verify(l => l.AssignAsync(It.IsAny<Order>()), Times.Never);
            }

            [Fact]
            public async Task AssignOrderToDelivery_StrategyThrowsException_ReturnsFailure()
            {
            // Arrange
            var order = new Order
            {
                OrderRef = "qeqweq",
                Customer = new Customer
                {
                    City = "Algiers",
                    FullName = "john smith",
                    Phone = "+213758049833"
                }
            };

            _Local.Setup(l => l.AssignAsync(order)).ThrowsAsync(new Exception("Strategy failure"));

                // Act
                var result = await _orderCommands.AssignOrderToDelivery(order);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("failed to Assign Order", result.Error);
            }
        

    }

}