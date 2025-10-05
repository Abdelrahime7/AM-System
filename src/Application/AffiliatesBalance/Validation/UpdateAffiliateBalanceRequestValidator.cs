using Application.AffiliatesBalance.DTOs;
using FluentValidation;

namespace Application.AffiliatesBalance.Validation;

public class UpdateAffiliateBalanceRequestValidator : AbstractValidator<UpdateAffiliateBalanceRequest>
{
    public UpdateAffiliateBalanceRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be a non-negative value.")
            .When(x => x.Amount.HasValue);
    }
}
