using Application.CallsLog.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CallsLog.validators
{
    public class CreateCallLogsRequestValidators : AbstractValidator<CreateCallLogRequest>
    {

        public CreateCallLogsRequestValidators()
        {
            RuleFor(x => x.CustomerPhone).NotEmpty().
                 WithMessage("Customer phone is required")
             .MinimumLength(12).WithMessage("Phone must be at least 12 character long")
             .Matches(@"^(\+213|0)(5|6|7)[0-9]{8}$")
             .WithMessage("Phone number must be a valid Algerian mobile number.");

            RuleFor(x => x.CallResult).IsInEnum()
                .NotEmpty().WithMessage("Call result is required");

            RuleFor(x => x.CalledAt)
                .NotEmpty().WithMessage("call date is required");

            RuleFor(x => x.OrderId).GreaterThan(0).
                NotEmpty().WithMessage("Order Id is required");


            RuleFor(x => x.AgentId).GreaterThan(0).
                NotEmpty().WithMessage("Agent Id is required");


        }
    }
}