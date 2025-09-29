using Application.Customers.DTOs;
using FluentValidation;

namespace Application.Customers.Validation;

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
            
        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MinimumLength(5).WithMessage("Full name must be at least 5 characters long.")
            .MaximumLength(50).WithMessage("Full name can't exceed 50 characters.");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("Customer city is required")
            .MinimumLength(2).WithMessage("City must be at least 2 character long")
            .MaximumLength(50).WithMessage("Full name can't exceed 50 characters.");
        
        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("Customer address is required")
            .MinimumLength(10).WithMessage("Address must be at least 10 character long")
            .MaximumLength(250).WithMessage("Full name can't exceed 250 characters.");

        RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("Customer phone is required")
            .MinimumLength(12).WithMessage("Phone must be at least 12 character long")
            .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$")
            .WithMessage("Phone number must be a valid Algerian mobile number.");
    }
}