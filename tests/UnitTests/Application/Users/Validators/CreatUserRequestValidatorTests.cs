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
            UserName = "",
            PasswordHash = "32321",
           Email = "old@example.com",
           FullName = "john doe",
           Phone = "0611223344",

       };

        public CreatUserRequestValidatorTests()
        {
            _validator = new CreatUserRequestValidator();
        }

     
        [Fact]
        public void Should_Have_Error_When_FullName_Is_Empty()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName)
                  .WithErrorMessage("Name is Required");
        }

      

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
           
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PasswordHash)
                  .WithErrorMessage("Password Must be at least 8 characters");
        }


        [Fact]
        public void Should_Not_Have_Errors_When_Request_Is_Valid()
        {
            var model1 = new CreateUserRequest
            {
                UserName = "Fatimma b",
                PasswordHash = "strgpass343",
                Email = "old@example.com",
                FullName = "john doe",
                Phone = "0611223344",


            };

            var result = _validator.TestValidate(model1);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

}
