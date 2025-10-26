using Application.Drivers.DTO_s;
using Application.Drivers.validation;
using FluentValidation.TestHelper;


namespace UnitTests.Application.Drivers.validators
{
    public class DriverValidatorTests
    {
        private readonly CreateDriverRequestValidator _validator;
        private readonly UpdateDriverRequestValidator _Uvalidator;

      
        public DriverValidatorTests()
        {
            _validator = new CreateDriverRequestValidator();
            _Uvalidator = new UpdateDriverRequestValidator();
        }

        [Fact]
        public void Should_Have_Error_When_IsLocal_Is_Null()
        {
            var model = new CreateDriverRequest
            {
                IsLocal = null,
                IsAvailable = true
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.IsLocal)
                  .WithErrorMessage("Local status must be specified.");
        }

        [Fact]
        public void Should_Have_Error_When_IsAvailable_Is_Null()
        {
            var model = new CreateDriverRequest
            {
                IsLocal = true,
                IsAvailable = null
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.IsAvailable)
                  .WithErrorMessage("Local status must be specified.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Both_Fields_Are_Valid()
        {
            var model = new CreateDriverRequest
            {
                IsLocal = true,
                IsAvailable = false
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.IsLocal);
            result.ShouldNotHaveValidationErrorFor(x => x.IsAvailable);
        }
        [Fact]
        public void Should_Not_Have_Error_When_IsLocal_Is_Specified()
        {
            var model = new UpdateDriverRequest
            {
                IsLocal = true,
                IsAvailable = null,
                UserID = 1
            };

            var result = _Uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.IsLocal);
        }
      
        [Fact]
        public void Should_Not_Have_Error_When_IsAvailable_Is_Specified()
        {
            var model = new UpdateDriverRequest
            {
                IsLocal = null,
                IsAvailable = false,
                UserID = 1
            };

            var result = _Uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.IsAvailable);
        }
        [Fact]
        public void Should_Have_Error_When_UserID_Is_Zero()
        {
            var model = new UpdateDriverRequest
            {
                IsLocal = true,
                IsAvailable = true,
                UserID = 0
            };

            var result = _Uvalidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserID)
                  .WithErrorMessage("User ID must be greater than 0");
        }
        [Fact]
        public void Should_Not_Have_Error_When_UserID_Is_Valid()
        {
            var model = new UpdateDriverRequest
            {
                IsLocal = true,
                IsAvailable = true,
                UserID = 5
            };

            var result = _Uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserID);
        }
    }




}


