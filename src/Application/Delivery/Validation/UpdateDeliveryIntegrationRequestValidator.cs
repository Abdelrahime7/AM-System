using Application.Delivery.DTOs;
using FluentValidation;

namespace Application.Customers.Validation;

public class UpdateDeliveryIntegrationRequestValidator : AbstractValidator<UpdateDeliveryIntegrationRequest>
{
    public UpdateDeliveryIntegrationRequestValidator()
    {

        RuleFor(x => x.Name)
            
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.ApiEndpoint)
           
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("API endpoint must be a valid URL.");

    }
}