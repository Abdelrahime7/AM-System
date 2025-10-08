using System;
using System.Collections.Generic;
using System.Linq;
using Application.Orders.DTOs;
using FluentValidation;

namespace Application.Orders.Validations
{
   

    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.OrderId)
              .GreaterThan(0)
              .When(x => x.OrderId != null)
              .WithMessage("Order ID must be a positive integer.");

            RuleFor(x => x.OrderRef)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.OrderRef))
                .WithMessage("Order reference must not exceed 50 characters.");

            RuleFor(x => x.OrderType)
                .IsInEnum()
                .When(x => x.OrderType != null || x.OrderType.HasValue)
                .WithMessage("Invalid order type.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .When(x => x.Status != null || x.Status.HasValue)
                .WithMessage("Invalid order status.");

            RuleFor(x => x.AffiliateId)
                .GreaterThan(0)
                .When(x => x.AffiliateId != null || x.AffiliateId.HasValue)
                .WithMessage("Affiliate ID must be a positive integer.");

            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .When(x => x.CustomerId != null ||x.CustomerId.HasValue)
                .WithMessage("Customer ID must be a positive integer.");

            RuleFor(x => x.DriverId)
                .GreaterThan(0)
                .When(x => x.DriverId != null|| x.DriverId.HasValue)
                .WithMessage("Driver ID must be a positive integer.");

            RuleFor(x => x.DeliveryCompanyId)
                .GreaterThan(0)
                .When(x => x.DeliveryCompanyId != null || x.DeliveryCompanyId.HasValue)
                .WithMessage("Delivery company ID must be a positive integer.");

            RuleFor(x => x.ReviewedBy)
                .GreaterThan(0)
                .When(x => x.ReviewedBy != null || x.ReviewedBy.HasValue)
                .WithMessage("Reviewer ID must be a positive integer.");

            RuleFor(x => x.ReviewedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.ReviewedAt != null || x.ReviewedAt.HasValue)
                .WithMessage("ReviewedAt cannot be in the future.");

            RuleFor(x => x.DepartedAt)
                .LessThanOrEqualTo(DateTime.UtcNow )
                .When(x => x.DepartedAt != null || x.DepartedAt.HasValue)
                .WithMessage("DepartedAt cannot be in the future.");

            RuleFor(x => x.DeliveredAt )
                .GreaterThanOrEqualTo(x => x.DepartedAt ?? DateTime.MinValue)
                .When(x => x.DeliveredAt != null || x.DeliveredAt.HasValue)
                .WithMessage("DeliveredAt must be after DepartedAt.");

        }
    }

}
