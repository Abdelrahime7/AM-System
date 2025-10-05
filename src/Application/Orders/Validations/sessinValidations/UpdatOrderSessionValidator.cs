using Application.CustomizedOrders.Validations;
using Application.OrderDetails.Validation;
using Application.Orders.DTOs.Session;
using FluentValidation;

namespace Application.Orders.Validations.sessinValidations
{
   

    public class UpdateOrderSessionValidator : AbstractValidator<UpdateOrderSession>
    {
        public UpdateOrderSessionValidator()
        {
            RuleFor(x => x.Order)
                .NotNull().WithMessage("Order update data is required.")
                .SetValidator(new UpdateOrderRequestValidator());

            RuleForEach(x => x.Customizations)
                .NotNull().WithMessage("Customization update entry cannot be null.")
                .SetValidator(new UpdateCustomizedOrderRequestValidator());

            RuleForEach(x => x.OrderDetails)
                .NotNull().WithMessage("Order detail update entry cannot be null.")
                .SetValidator(new UpdateOrderDetailRequestValidator());
        }
    }

}
