using Application.Drivers.DTO_s;
using FluentValidation;

namespace Application.Drivers.validation
{
    public class CreateDriverRequestValidator:AbstractValidator<CreateDriverRequest>
    {
     public   CreateDriverRequestValidator()
        {
            RuleFor(x => x.IsLocal)
                .NotNull()
                .WithMessage("Local status must be specified.");

            RuleFor(x => x.IsAvailable)
                .NotNull()
                .WithMessage("Local status must be specified.");
        }
            


    }
}
