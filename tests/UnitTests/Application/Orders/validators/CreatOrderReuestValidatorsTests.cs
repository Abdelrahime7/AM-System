using Application.Orders.DTOs;
using Application.Orders.Validations;
using Domain.Enums;
using FluentValidation.TestHelper;


namespace UnitTests.Application.Orders.validators
{
    public class CreateOrderRequestValidatorTests
    {
        private readonly CreateOrderRequestValidator _validator = new();

        [Fact]
        public void OrderRef_TooLong_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { OrderRef = new string('X', 51) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderRef)
                  .WithErrorMessage("Order reference must not exceed 50 characters.");
        }

        [Fact]
        public void OrderType_InvalidEnum_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { OrderType = (OrderType)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderType)
                  .WithErrorMessage("Invalid order type.");
        }

        [Fact]
        public void Status_InvalidEnum_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { Status = (OrderStatus)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Invalid order status.");
        }

        [Fact]
        public void AffiliateId_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { AffiliateId = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AffiliateId)
                  .WithErrorMessage("Affiliate ID must be a positive integer.");
        }

        [Fact]
        public void CustomerId_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { CustomerId = -1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CustomerId)
                  .WithErrorMessage("Customer ID must be a positive integer.");
        }

        [Fact]
        public void ReviewedAt_InFuture_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { ReviewedAt = DateTime.UtcNow.AddMinutes(5) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ReviewedAt)
                  .WithErrorMessage("ReviewedAt cannot be in the future.");
        }

        [Fact]
        public void DepartedAt_InFuture_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { DepartedAt = DateTime.UtcNow.AddHours(1) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DepartedAt)
                  .WithErrorMessage("DepartedAt cannot be in the future.");
        }

        [Fact]
        public void DeliveredAt_BeforeDepartedAt_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest
            {
                DepartedAt = DateTime.UtcNow,
                DeliveredAt = DateTime.UtcNow.AddMinutes(-10)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DeliveredAt)
                  .WithErrorMessage("DeliveredAt must be after DepartedAt.");
        }

        [Fact]
        public void DriverId_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { DriverId = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DriverId)
                  .WithErrorMessage("Driver ID must be a positive integer.");
        }

        [Fact]
        public void DeliveryCompanyId_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { DeliveryCompanyId = -5 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DeliveryCompanyId)
                  .WithErrorMessage("Delivery company ID must be a positive integer.");
        }

        [Fact]
        public void ReviewedBy_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new CreateOrderRequest { ReviewedBy = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ReviewedBy)
                  .WithErrorMessage("Reviewer ID must be a positive integer.");
        }

        [Fact]
        public void ValidModel_ShouldPassValidation()
        {
            var model = new CreateOrderRequest
            {
                OrderRef = "ORD-123",
                OrderType = OrderType.Product,
                Status = OrderStatus.Pending,
                AffiliateId = 1,
                CustomerId = 1,
                ReviewedAt = DateTime.UtcNow.AddMinutes(-5),
                DepartedAt = DateTime.UtcNow.AddMinutes(-10),
                DeliveredAt = DateTime.UtcNow,
                DriverId = 1,
                DeliveryCompanyId = 2,
                ReviewedBy = 3
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
