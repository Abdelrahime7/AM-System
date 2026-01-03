using Application.Users.DTOs;
using Domain.Enums;
using FluentValidation;

namespace Application.Users.validation
{
    public class CreatUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
      
        public CreatUserRequestValidator()
        {

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("Wrong Email format");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$")
            .WithMessage("Phone number must be a valid Algerian mobile number.");

            RuleFor(x => x.UserName).NotEmpty().
                WithMessage("UserName is required");

                RuleFor(x => x.Role).NotEmpty().IsInEnum().
                WithMessage("Role is required");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password Must be at least 8 characters");

         

      
       }
    }
}
