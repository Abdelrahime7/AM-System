using Application.Users.DTOs;
using Domain.Enums;
using FluentValidation;

namespace Application.Users.validation
{
    public class CreatUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
      
        public CreatUserRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name is Required");

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("Wrong Email format");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$")
            .WithMessage("Phone number must be a valid Algerian mobile number.");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password Must be at least 8 characters");

            RuleFor(x => x.CcpNumber)
            .NotEmpty().WithMessage("CCP number is required.")
            .Matches(@"^\d{8}-\d{2}$")
            .WithMessage("CCP number must be in the format 'XXXXXXXX-YY'.");

      
       }
    }
}
