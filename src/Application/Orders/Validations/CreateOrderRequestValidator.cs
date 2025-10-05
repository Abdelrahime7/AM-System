using Application.Orders.DTOs;
using FluentValidation;

namespace Application.Orders.Validations
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.OrderRef)
                .MaximumLength(50).WithMessage("Order reference must not exceed 50 characters.");

            RuleFor(x => x.OrderType)
                .IsInEnum().WithMessage("Invalid order type.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid order status.");

            RuleFor(x => x.AffiliateId)
                .GreaterThan(0).WithMessage("Affiliate ID must be a positive integer.");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be a positive integer.");

            RuleFor(x => x.ReviewedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.ReviewedAt.HasValue)
                .WithMessage("ReviewedAt cannot be in the future.");

            RuleFor(x => x.DepartedAt)
                .LessThanOrEqualTo(DateTime.UtcNow).When(x => x.DepartedAt.HasValue)
                .WithMessage("DepartedAt cannot be in the future.");

            RuleFor(x => x.DeliveredAt)
                .GreaterThanOrEqualTo(x => x.DepartedAt ?? DateTime.MinValue)
                .When(x => x.DeliveredAt.HasValue)
                .WithMessage("DeliveredAt must be after DepartedAt.");

            RuleFor(x => x.DriverId)
                .GreaterThan(0).When(x => x.DriverId.HasValue)
                .WithMessage("Driver ID must be a positive integer.");

            RuleFor(x => x.DeliveryCompanyId)
                .GreaterThan(0).When(x => x.DeliveryCompanyId.HasValue)
                .WithMessage("Delivery company ID must be a positive integer.");

            RuleFor(x => x.ReviewedBy)
                .GreaterThan(0).When(x => x.ReviewedBy.HasValue)
                .WithMessage("Reviewer ID must be a positive integer.");
        }
    }
}