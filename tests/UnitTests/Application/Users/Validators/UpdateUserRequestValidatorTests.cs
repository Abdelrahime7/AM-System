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
              
                PasswordHash = "weak",
               
                Status = (UserStatus)7,
              
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
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
          
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PasswordHash)
                  .WithErrorMessage("Password Must be at least 8 characters");
        }

      

        [Fact]
        public void Should_Have_Error_When_Status_Is_Invalid()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Status)
                  .WithErrorMessage("Status must be a valid value: Pending, Active, Inactive, Suspended");
        }


        [Fact]
        public void Should_Not_Have_Errors_When_Request_Is_Valid()
        {
            var model = new UpdateUserRequest
            {
                Id = 1,
             
                PasswordHash = "StrongPass123",
             
                Status = UserStatus.Active,
            
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
