using Application.Customers.DTOs;
using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Application.Orders.Validations.sessinValidations;
using Domain.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.Orders.validators
{
    public class CreatOrderSessionValidatorTests
    {
        private readonly CreatOrderSessionValidator _validator = new();

        [Fact]
        public void NullCustomer_ShouldHaveValidationError()
        {
            var session = new CreatOrderSession
            {
                Customer = null,
                Order = new CreateOrderRequest(),
                Customizations = [new CreateCustomizedOrderRequest
                {
                    CommissionAmount=100m,
                    Description=" normal mirorr ",
                    Status=Domain.Enums.CustomizedOrderStatus.Approved,
                    Dimensions="30*100",
                    ImageUrls=["",""],
                    Name="miror",
                    OrderId=1,
                    TotalPrice=200m
                }],
                OrderDetails = [new CreateOrderDetailRequest()]
            };

            var result = _validator.TestValidate(session);
            result.ShouldHaveValidationErrorFor(x => x.Customer)
                  .WithErrorMessage("Customer information is required.");
        }

        [Fact]
        public void NullOrder_ShouldHaveValidationError()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest { City="algies",
                FullName="johnSmithe",
                Phone="+213-544332211"},
                Order = null,
                Customizations = [new CreateCustomizedOrderRequest{
                    CommissionAmount=100m,
                    Description=" normal mirorr ",
                    Status=Domain.Enums.CustomizedOrderStatus.Approved,
                    Dimensions="30*100",
                    ImageUrls=["",""],
                    Name="miror",
                    OrderId=1,
                    TotalPrice=200m


                }
                
                ],
                OrderDetails = [new CreateOrderDetailRequest()]
            };

            var result = _validator.TestValidate(session);
            result.ShouldHaveValidationErrorFor(x => x.Order)
                  .WithErrorMessage("Order details are required.");
        }

        [Fact]
        public void NullCustomizationItem_ShouldHaveValidationError()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest {
                    City = "algies",
                    FullName = "johnSmithe",
                    Phone = "+213-544332211"
                },
                Order = new CreateOrderRequest(),
                Customizations = [null],
                OrderDetails = [new CreateOrderDetailRequest()]
            };

            var result = _validator.TestValidate(session);
            result.ShouldHaveValidationErrorFor("Customizations[0]")
                  .WithErrorMessage("Customization entry cannot be null.");
        }

        [Fact]
        public void NullOrderDetailItem_ShouldHaveValidationError()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest
                {
                    City = "algies",
                    FullName = "johnSmithe",
                    Phone = "+213-544332211"
                },
                Order = new CreateOrderRequest(),
                Customizations = [new CreateCustomizedOrderRequest {
                      CommissionAmount=100m,
                    Description=" normal mirorr ",
                    Status=Domain.Enums.CustomizedOrderStatus.Approved,
                    Dimensions="30*100",
                    ImageUrls=["",""],
                    Name="miror",
                    OrderId=1,
                    TotalPrice=200m
                }],
                OrderDetails = [null]
            };

            var result = _validator.TestValidate(session);
            result.ShouldHaveValidationErrorFor("OrderDetails[0]")
                  .WithErrorMessage("Order detail entry cannot be null.");
        }

        [Fact]
        public void ValidSession_ShouldPassValidation()
        {
            var session = new CreatOrderSession
            {
                Customer = new CreateCustomerRequest { FullName = "Ali samil",
                    City = "Algiers",
                    Address = "city 1000 residance"
                , Phone = "+213612345678"
                },
                Order = new CreateOrderRequest
                {
                    OrderRef = "ORD-123",
                    //OrderType = OrderType.Product,
                    //Status = OrderStatus.Pending,
                    AffiliateId = 1,
                    //CustomerId = 1,
                    ReviewedAt = DateTime.UtcNow.AddMinutes(-5),
                    DepartedAt = DateTime.UtcNow.AddMinutes(-10),
                    DeliveredAt = DateTime.UtcNow,
                    //DriverId = 1,
                    //DeliveryCompanyId = 2,
                    //ReviewedBy = 3
                    
                
                },
                Customizations = [new CreateCustomizedOrderRequest {   CommissionAmount=100m,
                    Description=" normal mirorr ",
                    Status=Domain.Enums.CustomizedOrderStatus.Approved,
                    Dimensions="30*100",
                    ImageUrls=["https://api.yourdomain.com/orders/42"],
                    Name="miror",
                    OrderId=1,
                    TotalPrice=200m,
                    
                }
                ],
                OrderDetails = [new CreateOrderDetailRequest { Quantity=1,
                OrderId=1,
                ProductId=1}]
            };

            var result = _validator.TestValidate(session);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
