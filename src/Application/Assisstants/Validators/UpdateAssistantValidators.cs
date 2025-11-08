using Application.Assisstants.Dto_s;
using FluentValidation;

namespace Application.Assisstants.Validators
{
    public class UpdateAssistantValidators:AbstractValidator<UpdateAssisstantRequest>

    {
        public UpdateAssistantValidators()
        {
            RuleFor(x => x.Id).NotEmpty().
                GreaterThan(0).WithMessage("Id should be greater than 0");

            RuleFor(x=>x.UserId).NotEmpty().When(x=>x.UserId != null).
                GreaterThan(0).WithMessage("UserId should be greater than 0") ;

            RuleFor(x => x.AssignedBy).NotEmpty().When(x => x.AssignedBy != null).
               GreaterThan(0).WithMessage("Assigned By admin refer to Id  should be greater than 0");
        }
    }
}
