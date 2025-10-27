using Application.Admins.Dto_s;
using FluentValidation;


namespace Application.Admins.Validators
{
    public class CreatAdminValidators :AbstractValidator<CreateAdminRequest>
    {
        public CreatAdminValidators()
        {
            RuleFor(x=>x.levels).IsInEnum().
                NotEmpty().WithMessage("Admin Access Level Required");
            
        }
    }
}
