using Application.Roles.DTOs;
using FluentValidation;

namespace Application.Roles.Validators;

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");

        RuleFor(x => x.RoleType)
            .IsInEnum().WithMessage("Invalid role type.")
            .When(x => x.RoleType.HasValue);
    }
}