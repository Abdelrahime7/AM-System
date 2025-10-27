using Application.Admins.Dto_s;
using FluentValidation;

namespace Application.Admins.Validators
{
    public class UpdateAdminValidators:AbstractValidator<UpdateAdminRequest>
    {
        public UpdateAdminValidators()
        {
            RuleFor(x => x.Id).NotEmpty().
                GreaterThan(0).WithMessage("ID should be greater than 0");
            RuleFor(x => x.levels).NotEmpty().
                When(x => x.levels != null);
        }
    }
}
