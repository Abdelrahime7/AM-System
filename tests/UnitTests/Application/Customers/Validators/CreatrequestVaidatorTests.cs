using Application.Customers.DTOs;
using Application.Customers.Validation;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Customers.Validators;

public class creatrequestVaidatorTests
{
    private readonly CreateCustomerRequestValidator _validator = new();
    private readonly UpdateCustomerRequestValidator _validator2 = new();

    [Fact]
    public void Should_HaveError_WhenIdIsEmpty()
    {
        var model = new UpdateCustomerRequest
        {
            Id = 0,
            FullName = null,
            City = null,
            Phone = null
        };
        var result = _validator2.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenIdIsNegative()
    {
        var model = new UpdateCustomerRequest
        {
            Id = -5,
            FullName = null,
            City = null,
            Phone = null
        };
        var result = _validator2.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenIdIsValid()
    {
        var model = new UpdateCustomerRequest
        {
            Id = 10,
            FullName = null,
            City = null,
            Phone = null
        };
        var result = _validator2.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }
    
    [Fact]
    public void Should_HaveError_WhenFullNameIsEmpty()
    {
        var model = new CreateCustomerRequest
        {
            FullName = "",
            City = null!,
            Phone = null!
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.FullName)
            .WithErrorMessage("Full name is required.");
    }

    [Fact]
    public void Should_HaveError_WhenFullNameTooShort()
    {
        var model = new CreateCustomerRequest
        {
            FullName = "Abc",
            City = null,
            Phone = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.FullName)
            .WithErrorMessage("Full name must be at least 5 characters long.");
    }

    [Fact]
    public void Should_HaveError_WhenFullNameTooLong()
    {
        var model = new CreateCustomerRequest
        {
            FullName = new string('A',
                51),
            City = null,
            Phone = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.FullName)
            .WithErrorMessage("Full name can't exceed 50 characters.");
    }

    [Fact]
    public void Should_HaveError_WhenCityIsEmpty()
    {
        var model = new CreateCustomerRequest
        {
            City = "",
            FullName = null,
            Phone = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.City)
            .WithErrorMessage("Customer city is required");
    }

    [Fact]
    public void Should_HaveError_WhenAddressTooShort()
    {
        var model = new CreateCustomerRequest
        {
            Address = "short",
            FullName = null,
            City = null,
            Phone = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Address)
            .WithErrorMessage("Address must be at least 10 character long");
    }

    [Fact]
    public void Should_HaveError_WhenPhoneIsInvalid()
    {
        var model = new CreateCustomerRequest
        {
            Phone = "1234567890",
            FullName = null,
            City = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Phone)
            .WithErrorMessage("Phone number must be a valid Algerian mobile number.");
    }

    [Fact]
    public void Should_NotHaveError_WhenRequestIsValid()
    {
        var model = new UpdateCustomerRequest
        {
            Id = 1,
            FullName = "Valid Customer",
            City = "Algiers",
            Address = "123 Valid Street, Algiers",
            Phone = "+213612345678"
        };

        var result = _validator2.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
