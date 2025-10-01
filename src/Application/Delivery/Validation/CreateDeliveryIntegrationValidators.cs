using Application.Customers.DTOs;
using Application.Delivery.DTOs;
using FluentValidation;

namespace Application.Customers.Validation;

public class CreateDeliveryIntegrationValidators : AbstractValidator<CreateDeliveryIntegrationRequest>
{
 
    public CreateDeliveryIntegrationValidators()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.ApiEndpoint)
            .NotEmpty().WithMessage("API endpoint is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("API endpoint must be a valid URL.");

        RuleFor(x => x.ApiKey)
            .NotEmpty().WithMessage("API key is required.");

        RuleFor(x => x.ApiSecret)
            .NotEmpty().WithMessage("API secret is required.");

        // Optional: Add logic for IsActive if needed
        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive must be specified."); // Only if we want to enforce explicit setting
    }
}

