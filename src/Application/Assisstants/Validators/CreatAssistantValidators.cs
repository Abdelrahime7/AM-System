using Application.Assisstants.Dto_s;
using FluentValidation;

namespace Application.Assisstants.Validators
{
    public class CreatAssistantValidators:AbstractValidator<CreatAssisstantRequest>

    {
        public CreatAssistantValidators()
        {
             RuleFor(x=>x.UserId).NotEmpty().
                GreaterThan(0).WithMessage("UserId should be greater than 0") ;

            RuleFor(x => x.AssignedBy).NotEmpty().
               GreaterThan(0).WithMessage("Assigned By admin refer to Id  should be greater than 0");
        }
    }
}
