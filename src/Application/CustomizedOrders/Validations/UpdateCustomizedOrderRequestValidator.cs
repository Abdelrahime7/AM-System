
using Application.CustomizedOrders.DTOs;
using FluentValidation;

namespace Application.CustomizedOrders.Validations
{
  

    public class UpdateCustomizedOrderRequestValidator : AbstractValidator<UpdateCustomizedOrderRequest>
    {
        public UpdateCustomizedOrderRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Name))
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Dimensions)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Dimensions))
                .WithMessage("Dimensions must not exceed 100 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().When(x => x.Status.HasValue ||x.Status!=null )
                .WithMessage("Invalid customized order status.");

            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0).When(x => x.TotalPrice.HasValue)
                .WithMessage("Total price must be non-negative.");

            RuleFor(x => x.CommissionAmount)
                .GreaterThanOrEqualTo(0).When(x => x.CommissionAmount.HasValue)
                .WithMessage("Commission amount must be non-negative.");

            RuleFor(x => x.OrderId)
                .GreaterThan(0).When(x => x.OrderId.HasValue||x.OrderId!=null)
                .WithMessage("Order ID must be a positive integer.");

            RuleForEach(x => x.ImageUrls)
                .NotEmpty().WithMessage("Image URL cannot be empty.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .When(x=>x.ImageUrls!=null)
                .WithMessage("Each image URL must be a valid absolute URI.");
        }
    }

}
