using Application.Drivers.DTO_s;
using FluentValidation;


namespace Application.Drivers.validation
{
    public class UpdateDriverRequestValidator:AbstractValidator<UpdateDriverRequest>
    {

        public UpdateDriverRequestValidator() {

            RuleFor(x => x.IsLocal)
                    .Must(_ => true)
                    .When(x => x.IsLocal != null)
                    .WithMessage("IsLocal was specified and passed validation.");

            RuleFor(x => x.IsAvailable)
                .Must(_ => true)
                .When(x => x.IsAvailable != null)
                .WithMessage("IsAvailable was specified and passed validation.");

            RuleFor(x => x.UserID).NotEmpty().GreaterThan(0).
                WithMessage("User ID must be greater than 0");

        }


    }

}
