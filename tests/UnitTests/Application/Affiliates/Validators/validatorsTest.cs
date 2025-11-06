

using Application.Affiliates.DTO_s;
using Application.Affiliates.Validation;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Affiliates.Validators
{
    public class ValidatorsTest
    {
        private readonly CreateRequestValidators _createValidator ;
        private readonly UpdateRequestValidators _UpdateValidator ;

        public ValidatorsTest()
        {
            _createValidator= new CreateRequestValidators();
            _UpdateValidator= new UpdateRequestValidators();
        }
       

          

        [Fact]
       

      
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var model = new UpdateAffiliateRequest { Id = 0 };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Id should be grater than 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var model = new UpdateAffiliateRequest { Id = 5 };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Have_Error_When_ReferralCode_Is_Zero()
        {
            var model = new UpdateAffiliateRequest { ReferralCode = 0 };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ReferralCode)
                  .WithErrorMessage("Referral Code required");
        }

       

        [Fact]
        public void Should_Not_Have_Error_When_ReferralCode_Is_Valid()
        {
            var model = new UpdateAffiliateRequest { ReferralCode = 123 };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.ReferralCode);
        }

        [Fact]
        public void Should_Have_Error_When_CommissionRate_Is_Null()
        {
            var model = new UpdateAffiliateRequest { CommissionRate = null };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CommissionRate)
                  .WithErrorMessage("Commission Rate required");
        }

        [Fact]
        public void Should_Not_Have_Error_When_CommissionRate_Is_Valid()
        {
            var model = new UpdateAffiliateRequest { CommissionRate = 0.25m };
            var result = _UpdateValidator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.CommissionRate);
        }
    }

}






