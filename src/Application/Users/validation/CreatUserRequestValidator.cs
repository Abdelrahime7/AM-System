using Application.Users.DTOs;
using Domain.Enums;
using FluentValidation;

namespace Application.Users.validation
{
    public class CreatUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
      
        public CreatUserRequestValidator()
        {
           
            RuleFor(x=>x.UserName).NotEmpty().
                WithMessage("UserName is required");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password Must be at least 8 characters");

         

      
       }
    }
}
