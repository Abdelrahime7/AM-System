using System;
using System.Collections.Generic;
using System.Linq;

using Application.OrderDetails.DTOs;
using FluentValidation;

namespace Application.OrderDetails.Validation
{

    public class CreateOrderDetailRequestValidator : AbstractValidator<CreateOrderDetailRequest>
    {
        public CreateOrderDetailRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

            RuleFor(x => x.UnitCommission)
                .GreaterThanOrEqualTo(0).WithMessage("Unit commission must be non-negative.");

            RuleFor(x => x.TotalPrice)
                .Equal(x => x.Quantity * x.UnitPrice)
                .WithMessage("Total price must equal Quantity × UnitPrice.");

            RuleFor(x => x.TotalCommission)
                .Equal(x => x.Quantity * x.UnitCommission)
                .WithMessage("Total commission must equal Quantity × UnitCommission.");

            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Order ID must be a positive integer.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("Product ID must be a positive integer.");
        }
    }

}
