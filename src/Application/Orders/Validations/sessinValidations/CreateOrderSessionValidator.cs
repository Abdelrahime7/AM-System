using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Validations.sessinValidations
{
    using Application.Customers.Validation;
    using Application.CustomizedOrders.Validations;
    using Application.OrderDetails.Validation;
    using Application.Orders.DTOs.Session;
    using FluentValidation;

    public class CreatOrderSessionValidator : AbstractValidator<CreatOrderSession>
    {
        public CreatOrderSessionValidator()
        {
            RuleFor(x => x.Customer)
                .NotNull().WithMessage("Customer information is required.")
                .SetValidator(new CreateCustomerRequestValidator());

            RuleFor(x => x.Order)
                .NotNull().WithMessage("Order details are required.")
                .SetValidator(new CreateOrderRequestValidator());


            //RuleForEach(x => x.Customizations)
            //    .SetValidator(new CreateCustomizedOrderRequestValidator());

            //RuleForEach(x => x.OrderDetails)
            //    .SetValidator(new CreateOrderDetailRequestValidator());
        }
    }

}
