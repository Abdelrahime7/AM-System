using Application.Common.Models;
using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.Interfaces.Common.Mappers;
using Application.Interfaces.CustomerInterfaces;
using Application.Interfaces.CustomizedOrderInterfaces;
using Application.Interfaces.OrderDetailInterfaces;
using Application.Interfaces.OrderInterfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWorks;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Application.Orders.Features.Commands;
using Domain.Entities;

using Moq;

namespace UnitTests.Application.Orders.Commands
{
    public partial class OrderCommandsTests
    {
        private readonly Mock<IOrderUnitOfWork> _UnitOfWorkMock=new();
        private readonly Mock<ICustomerCommands> _customerCommandsMock = new();
        private readonly Mock<IOrderDetailCommands> _orderDetailCommandsMock = new();
        private readonly Mock<ICustomizedOrderCommands> _customizedOrderCommandsMock = new();
        private readonly Mock<IOrderRepository> _orderRepoMock = new();

        private readonly Mock<ILocalDeliveryStrategy> _Local=new();
        private readonly Mock<IExternallDeliverStrategy> _External=new();

        private readonly Mock<IEntityMapper<Order, CreateOrderRequest, UpdateOrderRequest, OrderResponse>> _mapperMock = new();

        private readonly OrderCommands _orderCommands;

        public OrderCommandsTests()
        {

            _UnitOfWorkMock.SetupGet(u => u.Customers).Returns(_customerCommandsMock.Object);
            _UnitOfWorkMock.SetupGet(u => u.OrderDetails).Returns(_orderDetailCommandsMock.Object);
            _UnitOfWorkMock.SetupGet(u => u.CustomizedOrders).Returns(_customizedOrderCommandsMock.Object);
            _UnitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _orderCommands = new OrderCommands(

                _UnitOfWorkMock.Object,
                _Local.Object,
                _External.Object,
                _mapperMock.Object
            );
        }
     
            [Fact]
            public async Task CreateOrderAsync_ValidSession_ReturnsSuccessWithOrderId()
            {
                // Arrange
                var customer =  new CreateCustomerRequest { 
                    FullName="jOhn smith",
                    City="algiers",
                    Phone="+213799880965"
                    
                };
                var orderRequest = new CreateOrderRequest { OrderRef = "ORD-001" };
                var orderEntity = new Order { Id = 99,OrderRef=orderRequest.OrderRef };

                var session = new CreatOrderSession
                {
                    Customer = customer,
                    Order = orderRequest,
                    OrderDetails = new List<CreateOrderDetailRequest>(),
                    Customizations = new List<CreateCustomizedOrderRequest>()
                };

            
            
                _customerCommandsMock
                    .Setup(c => c.CreateCustomerAsync(customer))
                    .ReturnsAsync(Result<Customer>.Success( new Customer { City=customer.City,
                    FullName=customer.FullName,
                    Phone=customer.Phone,
                    }
                        ));

                _mapperMock
                    .Setup(m => m.ToEntity(orderRequest))
                    .Returns(orderEntity);

                _orderRepoMock
                    .Setup(r => r.AddAsync(orderEntity))
                    .Returns(Task.CompletedTask);

                _orderDetailCommandsMock
                    .Setup(d => d.AddRangeAsync(session.OrderDetails, orderEntity))
                    .ReturnsAsync(Result.Success);

                _customizedOrderCommandsMock
                    .Setup(c => c.AddRangeAsync(session.Customizations, orderEntity))
                    .ReturnsAsync(Result.Success);

                _UnitOfWorkMock
                    .SetupGet(u => u._orderRepository)
                    .Returns(_orderRepoMock.Object);

                // Act
                var result = await _orderCommands.CreateOrderAsync(session);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(99, result.Value);
                _orderRepoMock.Verify(r => r.AddAsync(orderEntity), Times.Once);
                _UnitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            }

            [Fact]
            public async Task CreateOrderAsync_NullOrderOrCustomer_ReturnsFailure()
            {
                // Arrange
                var session = new CreatOrderSession
                {
                    Customer = null,
                    Order = null
                };

                // Act
                var result = await _orderCommands.CreateOrderAsync(session);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("No order requests provided.", result.Error);
            }

            [Fact]
            public async Task CreateOrderAsync_CustomerCreationFails_ReturnsFailure()
            {
                // Arrange
                var session = new CreatOrderSession
                {
                    Customer = new CreateCustomerRequest
                    { City= "Algiers",FullName="john smith",
                     Phone="+213799886754"},
                    Order = new CreateOrderRequest()
                };

                _customerCommandsMock
                    .Setup(c => c.CreateCustomerAsync(session.Customer))
                    .ReturnsAsync(Result<Customer>.Failure("DB error"));

                // Act
                var result = await _orderCommands.CreateOrderAsync(session);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Customer creation failed.", result.Error);
            }

            [Fact]
            public async Task CreateOrderAsync_ThrowsException_ReturnsFailureWithMessage()
            {
                // Arrange
                var session = new CreatOrderSession
                {
                    Customer = new CreateCustomerRequest
                    {
                        City = "Algiers",
                        FullName = "john smith",
                        Phone = "+213799886754"
                    },
                    Order = new CreateOrderRequest()
                };

                _customerCommandsMock
                    .Setup(c => c.CreateCustomerAsync(session.Customer))
                    .ThrowsAsync(new Exception("Unexpected failure"));

                // Act
                var result = await _orderCommands.CreateOrderAsync(session);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("Failed to create order", result.Error);
            }
     }


}