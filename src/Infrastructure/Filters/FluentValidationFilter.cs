using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Filters
{
    public class FluentValidationFilter(IServiceProvider serviceProvider) : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        /// <summary>
        /// Executes before the controller action runs. 
        /// Validates each action argument using FluentValidation if a matching validator is registered.
        /// </summary>
        /// <param name="context">The context for the current action execution.</param>
        /// <remarks>
        /// If validation fails, the request is short-circuited and a 400 BadRequest is returned 
        /// with a structured list of validation errors.
        /// </remarks>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments.Values)
            {
                
                    if (arg == null) continue; // ✅ Prevent null reference

                    var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
                    var validator = _serviceProvider.GetService(validatorType) as IValidator;

                    if (validator != null)
                    {
                        var result = validator.Validate(new ValidationContext<object>(arg));
                        if (!result.IsValid)
                        {
                            context.Result = new BadRequestObjectResult(result.Errors.Select(e => new {
                                field = e.PropertyName,
                                message = e.ErrorMessage
                            }));
                            return;
                        }
                    }
                
            }
        }

        // this going to be implemented if we need to validat response .
        public void OnActionExecuted(ActionExecutedContext context) { }
    }

}
