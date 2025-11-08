

using Application.Assisstants.Dto_s;
using Application.Assisstants.Validators;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Assisstants.validators
{
   

    public class AssisstantValidatorTests
    {
        private readonly CreatAssistantValidators _validator = new();
        private readonly UpdateAssistantValidators _uvalidator = new();

        [Fact]
        public void Should_Have_Error_When_AssignedBy_Is_Null()
        {
            var model = new CreatAssisstantRequest { AssignedBy = null };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AssignedBy)
                  .WithErrorMessage("'Assigned By' must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_AssignedBy_Is_Zero()
        {
            var model = new CreatAssisstantRequest { AssignedBy = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AssignedBy)
                  .WithErrorMessage("Assigned By admin refer to Id  should be greater than 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_AssignedBy_Is_Valid()
        {
            var model = new CreatAssisstantRequest { AssignedBy = 42 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedBy);
        }
       


      

       

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new UpdateAssisstantRequest { Id = 0 };
            var result = _uvalidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Id should be greater than 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var model = new UpdateAssisstantRequest { Id = 5 };
            var result = _uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_UserId_Is_Null()
        {
            var model = new UpdateAssisstantRequest { UserId = null };
            var result = _uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_Have_Error_When_UserId_Is_Zero()
        {
            var model = new UpdateAssisstantRequest { UserId = 0 };
            var result = _uvalidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserId)
                  .WithErrorMessage("UserId should be greater than 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_UserId_Is_Valid()
        {
            var model = new UpdateAssisstantRequest { UserId = 10 };
            var result = _uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_AssignedBy_Is_Null()
        {
            var model = new UpdateAssisstantRequest { AssignedBy = null };
            var result = _uvalidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.AssignedBy);
        }

      

      
    }

}


