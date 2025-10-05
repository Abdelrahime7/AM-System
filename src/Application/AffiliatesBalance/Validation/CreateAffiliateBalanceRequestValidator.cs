using Application.AffiliatesBalance.DTOs;
using FluentValidation;

namespace Application.AffiliatesBalance.Validation;

public class CreateAffiliateBalanceRequestValidator : AbstractValidator<CreateAffiliateBalanceRequest>
{
    public CreateAffiliateBalanceRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be a non-negative value.");

        RuleFor(x => x.AffiliateId)
            .GreaterThan(0).WithMessage("AffiliateId must be greater than 0.");
    }
}
