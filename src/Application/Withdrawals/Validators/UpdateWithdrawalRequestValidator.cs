using Application.Withdrawals.DTOs;
using FluentValidation;

namespace Application.Withdrawals.Validators;

public class UpdateWithdrawalRequestValidator : AbstractValidator<UpdateWithdrawalRequest>
{
    public UpdateWithdrawalRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.")
            .When(x => x.Amount.HasValue);
    }
}
