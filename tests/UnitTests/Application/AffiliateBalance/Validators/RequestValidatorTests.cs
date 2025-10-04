using Application.AffiliatesBalance.DTOs;
using Application.AffiliatesBalance.Validation;
using FluentValidation.TestHelper;

namespace UnitTests.Application.AffiliateBalance.Validators;

public class RequestValidatorTests
{
    private readonly CreateAffiliateBalanceRequestValidator _createValidator = new();
    private readonly UpdateAffiliateBalanceRequestValidator _updateValidator = new();

    [Fact]
    public void Should_HaveError_WhenCreateAmountIsNegative()
    {
        var model = new CreateAffiliateBalanceRequest
        {
            Amount = -100m,
            AffiliateId = 42
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be a non-negative value.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateIdIsZero()
    {
        var model = new CreateAffiliateBalanceRequest
        {
            Amount = 1000m,
            AffiliateId = 0
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateId)
            .WithErrorMessage("AffiliateId must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateIdIsNegative()
    {
        var model = new CreateAffiliateBalanceRequest
        {
            Amount = 1000m,
            AffiliateId = -5
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateId)
            .WithErrorMessage("AffiliateId must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRequestIsValid()
    {
        var model = new CreateAffiliateBalanceRequest
        {
            Amount = 1500.75m,
            AffiliateId = 42
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsEmpty()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 0,
            Amount = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsNegative()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = -5,
            Amount = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateAmountIsNegative()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = -50m
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be a non-negative value.");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateAmountIsNull()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.Amount);
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValid()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = 2000.50m
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithZeroAmount()
    {
        var model = new UpdateAffiliateBalanceRequest
        {
            Id = 25,
            Amount = 0m
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}