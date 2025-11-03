using Application.Affiliates.DTO_s;
using FluentValidation;

namespace Application.Affiliates.Validation
{
    public class CreateRequestValidators:AbstractValidator<CreateAffiliateRequest>
    {
        public CreateRequestValidators()
        {
            RuleFor(x=>x.ReferralCode).NotEmpty().
                GreaterThan(0).WithMessage("Referral Code required");

            RuleFor(x => x.CommissionRate).NotEmpty().When(x => x!=null).
                WithMessage("Commission Rate required");

            RuleFor(x => x.PartnerSince).NotEmpty().When(x => x != null)
                .WithMessage("Partner Since date is required ");


        }

    }
}
