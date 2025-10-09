using Application.CustomizedOrders.DTOs;
using Application.OrderDetails.DTOs;
using Application.Orders.DTOs;
using Application.Orders.DTOs.Session;
using Application.Orders.Validations.sessinValidations;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Application.Orders.validators
{
    public class UpdateOrderSessionValidatorTests
    {
        private readonly UpdateOrderSessionValidator _validator = new();

        [Fact]
        public void NullOrder_ShouldHaveValidationError()
        {
            var model = new UpdateOrderSession
            {
                Order = null,
                Customizations = [new UpdateCustomizedOrderRequest()],
                OrderDetails = [new UpdateOrderDetailRequest()]
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Order)
                  .WithErrorMessage("Order update data is required.");
        }

        [Fact]
        public void NullCustomizationItem_ShouldHaveValidationError()
        {
            var model = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 1 },
                Customizations = [null],
                OrderDetails = [new UpdateOrderDetailRequest()]
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Customizations[0]")
                  .WithErrorMessage("Customization update entry cannot be null.");
        }

        [Fact]
        public void NullOrderDetailItem_ShouldHaveValidationError()
        {
            var model = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 1 },
                Customizations = [new UpdateCustomizedOrderRequest()],
                OrderDetails = [null]
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("OrderDetails[0]")
                  .WithErrorMessage("Order detail update entry cannot be null.");
        }

        [Fact]
        public void ValidSession_ShouldPassValidation()
        {
            var model = new UpdateOrderSession
            {
                Order = new UpdateOrderRequest { OrderId = 1 },
                Customizations = [new UpdateCustomizedOrderRequest()],
                OrderDetails = [new UpdateOrderDetailRequest {Id=1}]
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
