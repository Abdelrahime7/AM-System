using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.Repositories;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.Features.Queries;
using Domain.Entities;
using Moq;

namespace UnitTests.Application.Orders.Queries
{
    public partial class OrderQueriesTests
    {

        private readonly Mock<IOrderRepository> _orderRepoMock = new();
      
        private readonly Mock<IEntityMapper<Order, ChangeOrderStatus,
            UpdateOrderRequest, OrderResponse>> _mapperMock = new();

        private readonly Mock<IEntityMapper<Customer, CreateCustomerRequest, UpdateCustomerRequest
      , CustomerResponse>> _customermapper=new();

        private readonly Mock<IEntityMapper<CustomizedOrder, CreateCustomizedOrderRequest, UpdateCustomizedOrderRequest
       , CustomizedOrderResponse>>_customizedOrdermapper=new();

        private readonly Mock<IEntityMapper<OrderDetail, CreateOrderDetailRequest, UpdateOrderDetailRequest
       , OrderDetailResponse>> _orderDetailmapper = new();

        private readonly OrderQueries _orderQueries;

        public OrderQueriesTests()
        {
            _orderQueries = new OrderQueries(
                _orderRepoMock.Object,
                _mapperMock.Object,
                 _customermapper.Object,
                _customizedOrdermapper.Object,
                _orderDetailmapper.Object
               
                );
        }
        [Fact]
        public async Task GetOrderByIDAsync_ValidId_ReturnsSuccess()
        {
            var order = new Order
            {
                Id = 42,
                OrderRef="12131231231",

                Customer = new Customer { City="BISKRA",
                    FullName="john smith",
                 Phone="+213-544332211"},
                OrderDetails = [new OrderDetail()],
                Customizations = [new CustomizedOrder { Dimensions="30*120"
                ,Name="Mirorr"}]
            };

            var customerResponse = new CustomerResponse
            {
                FullName="john smith",
                City="biskra",
                Phone="+213-544332211"
            };
            var orderResponse = new OrderResponse();
            var detailResponse = new OrderDetailResponse();
            var customizationResponse = new CustomizedOrderResponse();

            _orderRepoMock.Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync(order);

            _customermapper.Setup(m => m.ToResponse(order.Customer))
                .Returns(customerResponse);

            _mapperMock.Setup(m => m.ToResponse(order))
                .Returns(orderResponse);

            _orderDetailmapper.Setup(m => m.ToResponse(It.IsAny<OrderDetail>()))
                .Returns(detailResponse);

            _customizedOrdermapper.Setup(m => m.ToResponse(It.IsAny<CustomizedOrder>()))
                .Returns(customizationResponse);

            var result = await _orderQueries.GetOrderByIDAsync(42);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(orderResponse, result.Value.Order);
            Assert.Equal(customerResponse, result.Value.Customer);
            Assert.Single(result.Value.OrderDetails);
            Assert.Single(result.Value.Customizations);
        }


        [Fact]
        public async Task GetOrderByIDAsync_OrderNotFound_ReturnsFailure()
        {
            _orderRepoMock.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((Order)null);

            var result = await _orderQueries.GetOrderByIDAsync(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Order not found", result.Error);
        }
        [Fact]
        public async Task GetOrderByIDAsync_ThrowsException_ReturnsFailure()
        {
            _orderRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("DB error"));

            var result = await _orderQueries.GetOrderByIDAsync(1);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to fetch order", result.Error);
        }


    }
}
