using Application.Users.DTOs;
using Application.Users.validation;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Users.Validators
{
    public class UpdateUserRequestValidatorTests
    {
        private readonly UpdateUserRequestValidator _validator;
        
        readonly   UpdateUserRequest model = new UpdateUserRequest
            {
                Id = 0,
                FullName = "",
                Email = "Invalid",
                Phone = "0551fvsd",
                PasswordHash = "weak",
                CcpNumber = "123d90",
                Status = (UserStatus)7,
                RoleId = 0
            };

        public UpdateUserRequestValidatorTests()
        {
            _validator = new UpdateUserRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("ID should be great than zero");
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
        public void Should_Have_Error_When_Status_Is_Invalid()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Status must be a valid value: Pending, Active, Inactive, Suspended");
        }

        [Fact]
        public void Should_Have_Error_When_RoleId_Is_Zero()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.RoleId)
                  .WithErrorMessage("RoleIdID should be great than zero");
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Request_Is_Valid()
        {
            var model = new UpdateUserRequest
            {
                Id = 1,
                FullName = "Fatima B.",
                Email = "fatima@example.com",
                Phone = "0551234567",
                PasswordHash = "StrongPass123",
                CcpNumber = "12345678-90",
                Status = UserStatus.Active,
                RoleId = 2
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
