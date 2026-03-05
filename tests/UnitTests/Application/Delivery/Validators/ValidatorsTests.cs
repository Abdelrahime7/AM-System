using Application.Customers.Validation;
using Application.Delivery.DTOs;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Delivery.Validator;

    public   class DeliveryValidatorsTests
    {
    private readonly CreateDeliveryIntegrationValidators _validator = new();
    private readonly UpdateDeliveryIntegrationRequestValidator _validator2 = new();


     private CreateDeliveryIntegrationRequest Creatmodel = new ()
        {
        
            Name = "SwiftExpress",
            ApiEndpoint = "https://api.swiftexpress.com/v1/integrations",
            ApiKey = "swiftexp-2025-key-abc123",
            ApiSecret = "s3cr3t!@#SecureKey",
            IsActive = true
        };
  

    [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
        Creatmodel.Name = "";
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_MaxLength()
        {
        Creatmodel.Name = new string('A', 101);
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_ApiEndpoint_Is_Empty()
        {
             Creatmodel. ApiEndpoint = "";
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.ApiEndpoint);
        }

        [Fact]
        public void Should_Have_Error_When_ApiEndpoint_Is_Invalid_Url()
        {
            Creatmodel . ApiEndpoint = "not-a-url" ;
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.ApiEndpoint);
        }

        [Fact]
        public void Should_Have_Error_When_ApiKey_Is_Empty()
        {
             Creatmodel . ApiKey = "" ;
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.ApiKey);
        }

        [Fact]
        public void Should_Have_Error_When_ApiSecret_Is_Empty()
        {
            Creatmodel .ApiSecret = "" ;
            var result = _validator.TestValidate(Creatmodel);
            result.ShouldHaveValidationErrorFor(x => x.ApiSecret);
        }

      
       

        [Fact]
        public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
        {
            var Creatmodel = new CreateDeliveryIntegrationRequest
            {
                Name = "FastShip",
                ApiEndpoint = "https://api.fastship.com",
                ApiKey = "abc123",
                ApiSecret = "secret!",
                IsActive = true
            };

            var result = _validator.TestValidate(Creatmodel);
            result.ShouldNotHaveAnyValidationErrors();
        }

    [Fact]
    public void UShould_Have_Error_When_Name_Exceeds_MaxLength()
    {
        var model = new UpdateDeliveryIntegrationRequest
        {
            Name = new string('A', 101) // 101 characters
        };

        var result = _validator2.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var model = new UpdateDeliveryIntegrationRequest
        {
            Name = "ValidName"
        };

        var result = _validator2.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void UShould_Have_Error_When_ApiEndpoint_Is_Invalid_Url()
    {
        var model = new UpdateDeliveryIntegrationRequest
        {
            ApiEndpoint = "not-a-valid-url"
        };

        var result = _validator2.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ApiEndpoint);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ApiEndpoint_Is_Valid_Url()
    {
        var model = new UpdateDeliveryIntegrationRequest
        {
            ApiEndpoint = "https://api.valid.com"
        };

        var result = _validator2.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ApiEndpoint);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ApiEndpoint_Is_Null()
    {
        var model = new UpdateDeliveryIntegrationRequest
        {
            ApiEndpoint = null
        };

        var result = _validator2.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ApiEndpoint);
    }
}





