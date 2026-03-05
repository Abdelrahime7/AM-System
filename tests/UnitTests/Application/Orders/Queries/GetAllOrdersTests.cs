using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Domain.Entities;
using Moq;
namespace UnitTests.Application.Orders.Queries
{
    public partial class OrderQueriesTests
    {
        [Fact]
        public async Task GetAllOrdersAsync_ReturnsMappedSessions()
        {
            // Arrange
            var orders = new List<Order>
    {
        new Order
        {
            Id = 1,
            OrderRef="o1212131231",
            Customer = new Customer{FullName="john Smith"
            ,City="Algiers",
            Phone="+213-544332211"},
            OrderDetails = new List<OrderDetail> { new OrderDetail() },
            Customizations = new List<CustomizedOrder> { new CustomizedOrder { 
                Dimensions="30*100",
                Name="Mirorr"
            } }
        },
        new Order
        {
           Id = 2,
            OrderRef="o1212131231",
            Customer = new Customer{FullName="john Smith"
            ,City="Algiers",
            Phone="+213-544332211"},
            OrderDetails = new List<OrderDetail> { new OrderDetail() },
            Customizations = new List<CustomizedOrder> { new CustomizedOrder {
                Dimensions="30*100",
                Name="Mirorr"

        }}
        }  
    };

            _orderRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(orders);

            _orderRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => orders.First(o => o.Id == id));

            _customermapper.Setup(m => m.ToResponse(It.IsAny<Customer>()))
                .Returns(new CustomerResponse { City = "Algiers",
                FullName="John Smith",
                Phone="+213-544332211"
                });

            _mapperMock.Setup(m => m.ToResponse(It.IsAny<Order>()))
                .Returns(new OrderResponse());

            _orderDetailmapper.Setup(m => m.ToResponse(It.IsAny<OrderDetail>()))
                .Returns(new OrderDetailResponse());

            _customizedOrdermapper.Setup(m => m.ToResponse(It.IsAny<CustomizedOrder>()))
                .Returns(new CustomizedOrderResponse());

            // Act
            var result = await _orderQueries.GetAllOrdersAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
            Assert.All(result.Value, session =>
            {
                Assert.NotNull(session.Customer);
                Assert.NotNull(session.Order);
            });
        }

        [Fact]
        public async Task GetAllOrdersAsync_NoOrders_ReturnsFailure()
        {
            _orderRepoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Order>());

            var result = await _orderQueries.GetAllOrdersAsync();

            Assert.False(result.IsSuccess);
            Assert.Equal("No Orders found", result.Error);
        }



    }
}
