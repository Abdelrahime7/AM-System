using Application.Roles.DTOs;
using Application.Roles.Validators;
using Domain.Enums;
using FluentValidation.TestHelper;

namespace UnitTests.Application.Roles.Validators;

public class RoleRequestValidatorTests
{
    private readonly CreateRoleRequestValidator _createValidator = new();
    private readonly UpdateRoleRequestValidator _updateValidator = new();

    [Fact]
    public void Should_HaveError_WhenCreateRoleTypeIsInvalid()
    {
        var model = new CreateRoleRequest
        {
            RoleType = (UserRole)999 // Invalid enum value
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.RoleType)
            .WithErrorMessage("Invalid role type.");
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRoleTypeIsValid()
    {
        var model = new CreateRoleRequest
        {
            RoleType = UserRole.Admin
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.RoleType);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    [InlineData(UserRole.Affiliate)]
    [InlineData(UserRole.Driver)]
    [InlineData(UserRole.AssistantAdmin)]
    [InlineData(UserRole.CallCenterAgent)]
    public void Should_NotHaveError_WhenCreateRoleTypeIsAnyValidEnum(UserRole roleType)
    {
        var model = new CreateRoleRequest
        {
            RoleType = roleType
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.RoleType);
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsZero()
    {
        var model = new UpdateRoleRequest
        {
            Id = 0,
            RoleType = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Role ID must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsNegative()
    {
        var model = new UpdateRoleRequest
        {
            Id = -5,
            RoleType = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Role ID must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateIdIsValid()
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    [Fact]
    public void Should_HaveError_WhenUpdateRoleTypeIsInvalid()
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = (UserRole)999 // Invalid enum value
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.RoleType)
            .WithErrorMessage("Invalid role type.");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRoleTypeIsNull()
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.RoleType);
    }

    [Theory]
    [InlineData(UserRole.Admin)]
    [InlineData(UserRole.Affiliate)]
    [InlineData(UserRole.Driver)]
    public void Should_NotHaveError_WhenUpdateRoleTypeIsAnyValidEnum(UserRole roleType)
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = roleType
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.RoleType);
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithRoleType()
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = UserRole.Admin
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithoutRoleType()
    {
        var model = new UpdateRoleRequest
        {
            Id = 1,
            RoleType = null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}