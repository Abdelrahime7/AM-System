using Application.Users.DTOs;
using Application.Users.validation;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Users.Validators
{
    public class CreatUserRequestValidatorTests
    {
        private readonly CreatUserRequestValidator _validator;
       readonly CreateUserRequest model = new CreateUserRequest
        {
            FullName = "",
            PasswordHash = "32321",
            Phone = "0122334455",
            Email = "usersaa",
           CcpNumber="1",
           
        };

        public CreatUserRequestValidatorTests()
        {
            _validator = new CreatUserRequestValidator();
        }

     
        [Fact]
        public void Should_Have_Error_When_FullName_Is_Empty()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FullName)
                  .WithErrorMessage("Name is Required");
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Wrong Email format");
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Is_Invalid()
        {
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Phone)
                  .WithErrorMessage("Phone number must be a valid Algerian mobile number.");
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PasswordHash)
                  .WithErrorMessage("Password Must be at least 8 characters");
        }

        [Fact]
        public void Should_Have_Error_When_CcpNumber_Is_Invalid()
        {
          
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CcpNumber)
                  .WithErrorMessage("CCP number must be in the format 'XXXXXXXX-YY'.");
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Request_Is_Valid()
        {
            var model1 = new CreateUserRequest
            {
                FullName = "Fatimma b",
                PasswordHash = "strgpass343",
                Phone = "0551234567",
                Email = "user@ex.com",
                CcpNumber="11128333-44"
                

            };

            var result = _validator.TestValidate(model1);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
