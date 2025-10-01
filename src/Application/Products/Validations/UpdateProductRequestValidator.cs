using Application.Products.DTOs;
using FluentValidation;

namespace Application.Products.Validations;

public class UpdateProductRequestValidator : AbstractValidator<UpdateAffiliateBalanceRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product ID must be provided.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.CommissionAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Commission amount cannot be negative.")
            .LessThanOrEqualTo(x => x.Price).WithMessage("Commission amount cannot exceed product price.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid product status.");

        RuleFor(x => x.Dimensions)
            .MaximumLength(200).WithMessage("Dimensions cannot exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.Dimensions));
    }
}