using Application.Orders.DTOs;
using Application.Orders.Validations;
using Domain.Enums;
using FluentValidation.TestHelper;


namespace UnitTests.Application.Orders.validators
{
    public class UpdateOrderRequestValidatorTests
    {
        private readonly UpdateOrderRequestValidator _validator = new();

        [Fact]
        public void OrderId_LessThanOrEqualZero_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderId)
                  .WithErrorMessage("Order ID must be a positive integer.");
        }

        [Fact]
        public void OrderRef_TooLong_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 1, OrderRef = new string('X', 51) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderRef)
                  .WithErrorMessage("Order reference must not exceed 50 characters.");
        }

        [Fact]
        public void OrderType_InvalidEnum_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 1, OrderType = (OrderType)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderType)
                  .WithErrorMessage("Invalid order type.");
        }

        [Fact]
        public void Status_InvalidEnum_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 1, Status = (OrderStatus)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Invalid order status.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AffiliateId_Invalid_ShouldHaveValidationError(int value)
        {
            var model = new UpdateOrderRequest { OrderId = 1, AffiliateId = value };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AffiliateId)
                  .WithErrorMessage("Affiliate ID must be a positive integer.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CustomerId_Invalid_ShouldHaveValidationError(int value)
        {
            var model = new UpdateOrderRequest { OrderId = 1, CustomerId = value };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CustomerId)
                  .WithErrorMessage("Customer ID must be a positive integer.");
        }

        [Fact]
        public void ReviewedAt_InFuture_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 1, ReviewedAt = DateTime.UtcNow.AddMinutes(10) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ReviewedAt)
                  .WithErrorMessage("ReviewedAt cannot be in the future.");
        }

        [Fact]
        public void DepartedAt_InFuture_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest { OrderId = 1, DepartedAt = DateTime.UtcNow.AddHours(1) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DepartedAt)
                  .WithErrorMessage("DepartedAt cannot be in the future.");
        }

        [Fact]
        public void DeliveredAt_BeforeDepartedAt_ShouldHaveValidationError()
        {
            var model = new UpdateOrderRequest
            {
                OrderId = 1,
                DepartedAt = DateTime.UtcNow,
                DeliveredAt = DateTime.UtcNow.AddMinutes(-5)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.DeliveredAt)
                  .WithErrorMessage("DeliveredAt must be after DepartedAt.");
        }

        [Fact]
        public void ValidModel_ShouldPassValidation()
        {
            var model = new UpdateOrderRequest
            {
                OrderId = 1,
                OrderRef = "ORD-123",
                OrderType = OrderType.Customized,
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
