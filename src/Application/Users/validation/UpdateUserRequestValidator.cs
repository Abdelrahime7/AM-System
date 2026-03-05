using Application.Users.DTOs;
using FluentValidation;

namespace Application.Users.validation
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
       
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                . When(x => x.Id != null)
                .WithMessage("ID is required")
                .GreaterThan(0).WithMessage("ID should be great than zero");

            RuleFor(x => x.FullName)
                .MinimumLength(5).When(x => x != null). 
                WithMessage("Full name must be at least 5 characters long.")
           .MaximumLength(50).WithMessage("Full name can't exceed 50 characters.");

            RuleFor(x => x.Email)
             .EmailAddress().When(x => x != null).
             WithMessage("Wrong Email format");

            RuleFor(x => x.Phone)
            .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$").
            When(x => x != null)
            .WithMessage("Phone number must be a valid Algerian mobile number.");


            RuleFor(x => x.Username).NotEmpty().
                When(x => x.Username != null).
               WithMessage("UserName is required");




            RuleFor(x => x.PasswordHash)
            .MinimumLength(8).When(x => x != null).
            WithMessage("Password Must be at least 8 characters");


            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid value: Pending, Active, Inactive, Suspended");

         



        }
    }
}
