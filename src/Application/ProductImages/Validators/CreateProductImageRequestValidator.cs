using Application.ProductImages.DTOs;
using FluentValidation;

namespace Application.ProductImages.Validators;

public class CreateProductImageRequestValidator : AbstractValidator<CreateProductImageRequest>
{
    public CreateProductImageRequestValidator()
    {
        RuleFor(x => x.ImageFile)
            .NotNull().WithMessage("Image file is required")
            .Must(file => file?.Length > 0).WithMessage("Image file cannot be empty")
            .Must(file => file?.Length <= MaxFileSizeInBytes)
                .WithMessage("Image file size exceeds the maximum limit of 5 MB")
            .Must(file => IsValidFileExtension(file?.FileName))
                .WithMessage($"Invalid file type. Allowed types: {string.Join(", ", AllowedExtensions)}");
        
        RuleFor(x => x.AltText)
            .MaximumLength(200).WithMessage("Alt text cannot exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.AltText));

        RuleFor(x => x)
            .Must(x => HaveValidAssociation(x.ProductId, x.CustomizedOrderId))
                .WithMessage("Image must be associated with either a product or a customized order, but not both.");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.")
            .When(x => x.ProductId.HasValue);

        RuleFor(x => x.CustomizedOrderId)
            .GreaterThan(0).WithMessage("Customized order ID must be greater than 0.")
            .When(x => x.CustomizedOrderId.HasValue);
    }
}