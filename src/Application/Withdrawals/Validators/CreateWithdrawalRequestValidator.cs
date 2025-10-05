using Application.Withdrawals.DTOs;
using FluentValidation;

namespace Application.Withdrawals.Validators;

public class CreateWithdrawalRequestValidator : AbstractValidator<CreateWithdrawalRequest>
{
    public CreateWithdrawalRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.AffiliateId)
            .GreaterThan(0).WithMessage("AffiliateId must be greater than 0.");

        RuleFor(x => x.AffiliateBalanceId)
            .GreaterThan(0).WithMessage("AffiliateBalanceId must be greater than 0.");
    }
}
