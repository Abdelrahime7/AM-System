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


            RuleFor(x => x.Username).NotEmpty().
                When(x => x.Username != null).
               WithMessage("UserName is required");




            RuleFor(x => x.PasswordHash)
            .MinimumLength(8).WithMessage("Password Must be at least 8 characters");


            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid value: Pending, Active, Inactive, Suspended");

         



        }
    }
}
