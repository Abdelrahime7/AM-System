using Application.CallsLog.DTOs;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CallsLog.validators
{
    using FluentValidation;

    public class UpdateCallLogRequestValidator : AbstractValidator<UpdateCallLogRequest>
    {
        public UpdateCallLogRequestValidator()
        {
            When(x => x.CustomerPhone is not null, () =>
            {
                RuleFor(x => x.CustomerPhone)
                    .NotEmpty().WithMessage("Customer phone cannot be empty.")
                    .MaximumLength(20).WithMessage("Customer phone must not exceed 20 characters.")
                    .Matches(@"^\+?\d{7,15}$").WithMessage("Customer phone must be a valid international number.");
            });

            When(x => x.CallResult.HasValue, () =>
            {
                RuleFor(x => x.CallResult)
                    .IsInEnum().WithMessage("Call result must be a valid enum value.");
            });

            When(x => x.CalledAt.HasValue, () =>
            {
                RuleFor(x => x.CalledAt.Value)
                    .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Call timestamp cannot be in the future.");
            });

            When(x => x.OrderId.HasValue, () =>
            {
                RuleFor(x => x.OrderId.Value)
                    .GreaterThan(0).WithMessage("Order ID must be a positive number.");
            });

            When(x => x.AgentId.HasValue, () =>
            {
                RuleFor(x => x.AgentId.Value)
                    .GreaterThan(0).WithMessage("Agent ID must be a positive number.");
            });
        }
    }

}

