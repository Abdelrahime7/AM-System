using Application.OrderDetails.DTOs;
using FluentValidation;

namespace Application.OrderDetails.Validation
{
   
    public class UpdateOrderDetailRequestValidator : AbstractValidator<UpdateOrderDetailRequest>
    {
        public UpdateOrderDetailRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Detail ID must be a positive integer.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).When(x => x.Quantity.HasValue)
                .WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).When(x => x.UnitPrice.HasValue)
                .WithMessage("Unit price must be non-negative.");

            RuleFor(x => x.UnitCommission)
                .GreaterThanOrEqualTo(0).When(x => x.UnitCommission.HasValue)
                .WithMessage("Unit commission must be non-negative.");

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0).When(x => x.TotalPrice.HasValue)
                .WithMessage("Total price must be non-negative.");

            RuleFor(x => x.TotalCommission)
                .GreaterThanOrEqualTo(0).When(x => x.TotalCommission.HasValue)
                .WithMessage("Total commission must be non-negative.");

            RuleFor(x => x.OrderId)
                .GreaterThan(0).When(x => x.OrderId.HasValue||x.OrderId!=null)
                .WithMessage("Order ID must be a positive integer.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).When(x => x.ProductId.HasValue||x.ProductId!=null)
                .WithMessage("Product ID must be a positive integer.");

            RuleFor(x => x)
                .Must(x =>
                    !(x.Quantity.HasValue && x.UnitPrice.HasValue && x.TotalPrice.HasValue) ||
                    x.TotalPrice == x.Quantity * x.UnitPrice
                )
                .WithMessage("Total price must equal Quantity × UnitPrice when all are provided.");

            RuleFor(x => x)
                .Must(x =>
                    !(x.Quantity.HasValue && x.UnitCommission.HasValue && x.TotalCommission.HasValue) ||
                    x.TotalCommission == x.Quantity * x.UnitCommission
                )
                .WithMessage("Total commission must equal Quantity × UnitCommission when all are provided.");
        }
    }

}
