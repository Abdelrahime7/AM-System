using Application.Withdrawals.DTOs;
using Application.Withdrawals.Validators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Withdrawals.Validators;

public class WithdrawalRequestValidatorTests
{
    private readonly CreateWithdrawalRequestValidator _createValidator = new();
    private readonly UpdateWithdrawalRequestValidator _updateValidator = new();

    [Fact]
    public void Should_HaveError_WhenCreateAmountIsZero()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 0,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAmountIsNegative()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = -100m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateIdIsZero()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 0,
            AffiliateBalanceId = 25
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateId)
            .WithErrorMessage("AffiliateId must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateIdIsNegative()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = -5,
            AffiliateBalanceId = 25
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateId)
            .WithErrorMessage("AffiliateId must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateBalanceIdIsZero()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 0
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateBalanceId)
            .WithErrorMessage("AffiliateBalanceId must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateAffiliateBalanceIdIsNegative()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = -5
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AffiliateBalanceId)
            .WithErrorMessage("AffiliateBalanceId must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRequestIsValid()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25,
            ProcessedBy = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRequestIsValidWithoutProcessedBy()
    {
        var model = new CreateWithdrawalRequest
        {
            Amount = 500.75m,
            Status = WithdrawalStatus.Pending,
            AffiliateId = 42,
            AffiliateBalanceId = 25
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsEmpty()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 0,
            Amount = null,
            Status = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsNegative()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = -5,
            Amount = null,
            Status = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateAmountIsZero()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = 0
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateAmountIsNegative()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = -50m
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
            .WithErrorMessage("Amount must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateAmountIsNull()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = null,
            Status = WithdrawalStatus.Approved
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.Amount);
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValid()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = 750.50m,
            Status = WithdrawalStatus.Approved,
            ProcessedAt = DateTime.UtcNow,
            AffiliateId = 42,
            AffiliateBalanceId = 25,
            ProcessedBy = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithPartialFields()
    {
        var model = new UpdateWithdrawalRequest
        {
            Id = 10,
            Amount = null,
            Status = WithdrawalStatus.Approved,
            ProcessedAt = null,
            AffiliateId = null,
            AffiliateBalanceId = null,
            ProcessedBy = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}