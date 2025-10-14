using System;

using Application.CustomizedOrders.DTOs;
using FluentValidation;

namespace Application.CustomizedOrders.Validations
{

    public class CreateCustomizedOrderRequestValidator : AbstractValidator<CreateCustomizedOrderRequest>
    {
      
            public CreateCustomizedOrderRequestValidator()
            {
                // Only validate if the request is not null (i.e., customized order is present)
                When(x => x != null, () =>
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Name is required.")
                        .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

                    RuleFor(x => x.Description)
                        .NotEmpty().WithMessage("Description is required.")
                        .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

                    RuleFor(x => x.Dimensions)
                        .NotEmpty().WithMessage("Dimensions are required.")
                        .MaximumLength(100).WithMessage("Dimensions must not exceed 100 characters.");

                    RuleFor(x => x.Status)
                        .IsInEnum().WithMessage("Invalid customized order status.");

                    //RuleFor(x => x.TotalPrice)
                    //    .GreaterThanOrEqualTo(0).WithMessage("Total price must be non-negative.");

                    //RuleFor(x => x.CommissionAmount)
                    //    .GreaterThanOrEqualTo(0).WithMessage("Commission amount must be non-negative.");

                    RuleFor(x => x.OrderId)
                        .GreaterThan(0).WithMessage("Order ID must be a positive integer.");

                    RuleForEach(x => x.ImageUrls)
                        .NotEmpty().WithMessage("Image URL cannot be empty.")
                        .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                        .WithMessage("Each image URL must be a valid absolute URI.");
                });
            }
        }

    }


