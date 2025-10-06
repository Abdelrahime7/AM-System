using Application.Roles.DTOs;
using FluentValidation;

namespace Application.Roles.Validators;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(x => x.RoleType)
            .IsInEnum().WithMessage("Invalid role type.");
    }
}