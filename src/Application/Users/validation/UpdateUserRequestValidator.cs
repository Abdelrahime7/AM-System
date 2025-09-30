using Application.Users.DTOs;
using FluentValidation;

namespace Application.Users.validation
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
       
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required")
                .GreaterThan(0).WithMessage("ID should be great than zero");

            RuleFor(x => x.FullName)
                 .MinimumLength(5).WithMessage("Full name must be at least 5 characters long.")
            .MaximumLength(50).WithMessage("Full name can't exceed 50 characters.");

            RuleFor(x => x.Email)
             .EmailAddress().WithMessage("Wrong Email format");

            RuleFor(x => x.Phone)
            .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$")
            .WithMessage("Phone number must be a valid Algerian mobile number.");

            RuleFor(x => x.PasswordHash)
            .MinimumLength(8).WithMessage("Password Must be at least 8 characters");

            RuleFor(x => x.CcpNumber)
           
            .Matches(@"^\d{8}-\d{2}$")
            .WithMessage("CCP number must be in the format 'XXXXXXXX-YY'.");

            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid value: Pending, Active, Inactive, Suspended");

            RuleFor(x => x.RoleId).NotEmpty()
             .GreaterThan(0).WithMessage("RoleIdID should be great than zero");



        }
    }
}
