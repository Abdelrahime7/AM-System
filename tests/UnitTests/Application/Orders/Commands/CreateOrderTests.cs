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
using Application.Orders.Delivery;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Application.Orders.Features.Commands;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Repositories;
using Moq;

namespace UnitTests.Application.Orders.Commands
{
    public partial class OrderCommandsTests
    {
        private readonly Mock<IOrderUnitOfWork> _UnitOfWorkMock;
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
        public async Task CreatOrderAsync_NullOrderOrCustomer_ReturnsFailure()
        {
            var session = new CreatOrderSession { Order = null, Customer = null };

            var result = await _orderCommands.CreateOrderAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("No order requests provided.", result.Error);
        }

        [Fact]
        public async Task CreatOrderAsync_CustomerCreationFails_ReturnsFailure()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest
                {
                    FullName = "",
                    City = "algiers",
                    Phone = "+213-566644433"

                },
                Order = new CreateOrderRequest()
            };

            _customerCommandsMock.Setup(c => c.CreateCustomerAsync(session.Customer))
                .ReturnsAsync(Result<Customer>.Failure("Customer creation failed."));

            var result = await _orderCommands.CreateOrderAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Equal("Customer creation failed.", result.Error);
        }

        [Fact]
        public async Task CreatOrderAsync_ValidSession_ReturnsSuccess()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest
                {
                    FullName = "",
                    City = "algiers",
                    Phone = "+213-566644433"
                },
                Order = new CreateOrderRequest(),
                OrderDetails = [new CreateOrderDetailRequest()],
                Customizations = [new CreateCustomizedOrderRequest {
         // CommissionAmount = 100m,
            Description = "",
            Dimensions = "30*100",
            ImageUrls = [],
            Name = "",
            OrderId = 1,
            Status = CustomizedOrderStatus.Approved,
         // TotalPrice = 111000m
        }]
            };

            var orderEntity = new Order
            {
                Id = 42,
                OrderRef = "e122653712531"
            };

            _customerCommandsMock.Setup(c => c.CreateCustomerAsync(session.Customer))
                .ReturnsAsync(Result<Customer>.Success(new Customer{
                    FullName=session.Customer.FullName,
                    Address=session.Customer.Address,
                    City=session.Customer.City,
                    Phone=session.Customer.Phone,
                }));

            _mapperMock.Setup(m => m.ToEntity(session.Order))
                .Returns(orderEntity);

            _orderRepoMock.Setup(r => r.AddAsync(orderEntity))
                .Returns(Task.CompletedTask);

            _orderDetailCommandsMock.Setup(d => d.AddRangeAsync(It.IsAny<List<CreateOrderDetailRequest>>()))
                .ReturnsAsync(Result.Success());

            _customizedOrderCommandsMock.Setup(c => c.AddRangeAsync(It.IsAny<List<CreateCustomizedOrderRequest>>()))
                .ReturnsAsync(Result.Success());

            _orderRepoMock.Setup(r => r.CommitAsync(default))
                .Returns(Task.CompletedTask);

            var result = await _orderCommands.CreateOrderAsync(session);

            Assert.True(result.IsSuccess);
            Assert.Equal(42, result.Value);
        }


        [Fact]
        public async Task CreatOrderAsync_ThrowsException_ReturnsFailure()
        {
            var session = new CreatOrderSession
            {
                Customer =
                new CreateCustomerRequest
                {
                    FullName = "",
                    City = "algiers",
                    Phone = "+213-566644433"
                },
                Order = new CreateOrderRequest()
            };

            _customerCommandsMock.Setup(c => c.CreateCustomerAsync(session.Customer))
                .ThrowsAsync(new Exception("Unexpected DB error"));

            var result = await _orderCommands.CreateOrderAsync(session);

            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to create order", result.Error);
        }
    }
}