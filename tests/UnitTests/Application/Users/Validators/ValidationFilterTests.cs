using Application.Users.DTOs;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

namespace UnitTests.Application.Users.Validators
{
    public class ValidationFilterTests
    {
        CreateUserRequest request = 
            new CreateUserRequest{
            FullName = "",
            PasswordHash = "32321",
            Phone = "0122334455",
            Email = "usersaa",
           CcpNumber="1",
                               };
        [Fact]
        public void OnActionExecuting_ShouldSetBadRequest_WhenValidationFails()
        {

           

            var validatorMock = new Mock<IValidator<CreateUserRequest>>();
            validatorMock
                .Setup(v => v.Validate(It.IsAny<IValidationContext>()))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
        new ValidationFailure("FullName", "Name is Required"),
        new ValidationFailure("PasswordHash", "Password Must be at least 8 characters"),
        new ValidationFailure("Phone", "Phone number must be a valid Algerian mobile number."),
        new ValidationFailure("Email", "Wrong Email format"),
        new ValidationFailure("CcpNumber", "CCP number must be in the format 'XXXXXXXX-YY'")
                }));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IValidator<CreateUserRequest>)))
                .Returns(validatorMock.Object);

            var filter = new FluentValidationFilter(serviceProviderMock.Object);

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
               new Microsoft.AspNetCore.Routing.RouteData(),
                new ActionDescriptor(),
                new ModelStateDictionary()
            );

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object> { { "request", request } },
                controller: null
            );

            // Act
            filter.OnActionExecuting(context);

            // Assert
            Assert.NotNull(context.Result);
            Assert.IsType<BadRequestObjectResult>(context.Result);

            var badRequest = context.Result as BadRequestObjectResult;
            var errors = badRequest.Value as IEnumerable<object>;

            Assert.NotNull(errors);
            Assert.Equal(5, errors.Count()); // Expecting 5 validation errors



        }

        [Fact]
        public void OnActionExecuting_ShouldNotSetResult_WhenValidationPasses()
        {
            // Arrange
            var validRequest = new CreateUserRequest
            {
                FullName = "Fatima",
                Email = "fatima@example.com",
                Phone = "0551234567",
                PasswordHash = "StrongPass123",
                CcpNumber = "12345678-90"
            };

            var validatorMock = new Mock<IValidator<CreateUserRequest>>();
            validatorMock
                      .Setup(v => v.Validate(It.IsAny<IValidationContext>()))
                       .Returns(new ValidationResult());
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(IValidator<CreateUserRequest>)))
                .Returns(validatorMock.Object);

         

            var actionArguments = new Dictionary<string, object>
             {
               { "request", validRequest }
             };

            var filter = new FluentValidationFilter(serviceProviderMock.Object);

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
               new Microsoft.AspNetCore.Routing.RouteData(),
                new ActionDescriptor(),
                new ModelStateDictionary()
            );

            var context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                actionArguments,
                controller: null
            );

            // Act
            filter.OnActionExecuting(context);

            // Assert
            Assert.Null(context.Result); // pipeline should continue
        }
    }

}
