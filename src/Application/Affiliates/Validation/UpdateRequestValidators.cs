using Application.Affiliates.DTO_s;
using FluentValidation;

namespace Application.Affiliates.Validation
{
    public class UpdateRequestValidators:AbstractValidator<UpdateAffiliateRequest>
    {
        public UpdateRequestValidators()
        {
            RuleFor(x=>x.Id).NotEmpty()
                   .GreaterThan(0).WithMessage("Id should be grater than 0");

            RuleFor(x => x.ReferralCode).NotEmpty().When(x => x != null).
                GreaterThan(0).WithMessage("Referral Code required");

            RuleFor(x => x.CommissionRate).NotEmpty().When(x => x != null).
                WithMessage("Commission Rate required");

        }
    }
}
