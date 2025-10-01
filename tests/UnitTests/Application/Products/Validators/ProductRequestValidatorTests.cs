using Application.Products.DTOs;
using Application.Products.Validations;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Products.Validators;

public class ProductRequestValidatorTests
{
    private readonly CreateProductRequestValidator _createValidator = new();
    private readonly UpdateProductRequestValidator _updateValidator = new();

    // CreateProductRequestValidator Tests
    [Fact]
    public void Create_Should_HaveError_When_Name_Is_Empty()
    {
        var model = new CreatetAffiliateBalanceRequest { Name = "" };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Product name is required.");
    }

    [Fact]
    public void Create_Should_HaveError_When_Name_Is_Too_Long()
    {
        var model = new CreatetAffiliateBalanceRequest { Name = new string('a', 101) };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Product name cannot exceed 100 characters.");
    }

    [Fact]
    public void Create_Should_HaveError_When_Price_Is_Not_Greater_Than_Zero()
    {
        var model = new CreatetAffiliateBalanceRequest { Name = "Test", Price = 0 };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Price).WithErrorMessage("Price must be greater than zero.");
    }

    [Fact]
    public void Create_Should_HaveError_When_Commission_Is_Negative()
    {
        var model = new CreatetAffiliateBalanceRequest { Name = "Test", Price = 10, CommissionAmount = -1 };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CommissionAmount).WithErrorMessage("Commission amount cannot be negative.");
    }

    [Fact]
    public void Create_Should_HaveError_When_Commission_Exceeds_Price()
    {
        var model = new CreatetAffiliateBalanceRequest { Name = "Test", Price = 10, CommissionAmount = 11 };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.CommissionAmount).WithErrorMessage("Commission amount cannot exceed product price.");
    }

    // UpdateProductRequestValidator Tests
    [Fact]
    public void Update_Should_HaveError_When_Id_Is_Zero()
    {
        var model = new UpdateAffiliateBalanceRequest { Id = 0 };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage("Product ID must be provided.");
    }

    [Fact]
    public void Update_Should_Not_HaveError_When_Name_Is_Null()
    {
        var model = new UpdateAffiliateBalanceRequest { Id = 1, Name = null };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Update_Should_HaveError_When_Name_Is_Too_Long()
    {
        var model = new UpdateAffiliateBalanceRequest { Id = 1, Name = new string('a', 101) };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage("Product name cannot exceed 100 characters.");
    }
    
    [Fact]
    public void Update_Should_Not_Have_Any_Validation_Errors_When_Request_Is_Valid()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 1,
            Name = "Valid Name",
            Price = 100,
            CommissionAmount = 10,
            Status = ProductStatus.Active
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
