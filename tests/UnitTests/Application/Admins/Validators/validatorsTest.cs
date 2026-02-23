

using Application.Admins.Dto_s;
using Application.Admins.Validators;
using Domain.Enums;

namespace UnitTests.Application.Admins.Validators
{
    public class ValidatorsTest
    {
        private readonly CreatAdminValidators _createValidator ;
        private readonly UpdateAdminValidators _UpdateValidator ;

        public ValidatorsTest()
        {
            _createValidator= new CreatAdminValidators();
            _UpdateValidator= new UpdateAdminValidators();
        }
       

            [Fact]
            public void Should_Fail_When_Levels_Is_Null()
            {
                // Arrange
                var request = new CreateAdminRequest
                {
                    levels = null // assuming nullable enum
                };

                // Act
                var result = _createValidator.Validate(request);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "levels" && e.ErrorMessage == "Admin Access Level Required");
            }

            [Fact]
            public void Should_Fail_When_Levels_Is_InvalidEnum()
            {
                // Arrange
                var request = new CreateAdminRequest
                {
                    levels = (AccessLevels)999 // assuming 999 is not defined in the enum
                };

                // Act
                var result = _createValidator.Validate(request);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "levels");
            }

            [Fact]
            public void Should_Pass_When_Levels_Is_ValidEnum()
            {
                // Arrange
                var request = new CreateAdminRequest
                {
                    levels = AccessLevels.Admin // assuming this is a valid enum value
                };

                // Act
                var result = _createValidator.Validate(request);

                // Assert
                Assert.True(result.IsValid);
            }
       

            [Fact]
            public void Should_Fail_When_Id_Is_Zero()
            {
                var request = new UpdateAdminRequest { Id = 0 };
                var result = _UpdateValidator.Validate(request);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "ID should be greater than 0");
            }

            [Fact]
            public void Should_Fail_When_Id_Is_Negative()
            {
                var request = new UpdateAdminRequest { Id = -5 };
                var result = _UpdateValidator.Validate(request);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage == "ID should be greater than 0");
            }

            [Fact]
            public void Should_Pass_When_Id_Is_Positive()
            {
                var request = new UpdateAdminRequest { Id = 10 };
                var result = _UpdateValidator.Validate(request);

                Assert.True(result.IsValid);
            }

            [Fact]
            public void Should_Fail_When_Levels_Is_Nullable_And_Empty()
            {
                var request = new UpdateAdminRequest { Id = 1, levels = null };
                var result = _UpdateValidator.Validate(request);

                // Should pass because rule only applies when levels != null
                Assert.True(result.IsValid);
            }

            [Fact]
          
   
            public void Should_Pass_When_Levels_Is_Valid()
            {
                var request = new UpdateAdminRequest { Id = 1, levels = AccessLevels.Admin };
                var result = _UpdateValidator.Validate(request);

                Assert.True(result.IsValid);
            }
        

    }

}
