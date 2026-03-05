using Application.ProductImages.DTOs;
using Application.ProductImages.Validators;
using Microsoft.AspNetCore.Http;
using Moq;
using FluentValidation.TestHelper;

namespace UnitTests.Application.ProductImages.Validators;

public class ProductImageRequestValidatorTests
{
    private readonly CreateProductImageRequestValidator _createValidator = new();
    private readonly UpdateProductImageRequestValidator _updateValidator = new();

    [Fact]
    public void Should_HaveError_WhenCreateImageFileIsNull()
    {
        var model = new CreateProductImageRequest
        {
            ImageFile = null!,
            AltText = "Test image",
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ImageFile)
            .WithErrorMessage("Image file is required");
    }

    [Fact]
    public void Should_HaveError_WhenCreateImageFileIsEmpty()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(0);

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ImageFile)
            .WithErrorMessage("Image file cannot be empty");
    }

    [Fact]
    public void Should_HaveError_WhenCreateImageFileTooLarge()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(6 * 1024 * 1024); // 6MB
        mockFile.Setup(f => f.FileName).Returns("large-image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ImageFile)
            .WithErrorMessage("Image file size exceeds the maximum limit of 5 MB");
    }

    [Fact]
    public void Should_HaveError_WhenCreateImageFileInvalidExtension()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("document.pdf");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ImageFile)
            .WithErrorMessage("Invalid file type. Allowed types: .jpg, .jpeg, .png, .gif, .webp");
    }

    [Theory]
    [InlineData("image.jpg")]
    [InlineData("photo.jpeg")]
    [InlineData("picture.png")]
    [InlineData("animation.gif")]
    [InlineData("graphic.webp")]
    public void Should_NotHaveError_WhenCreateImageFileValidExtension(string fileName)
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns(fileName);

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.ImageFile);
    }

    [Fact]
    public void Should_HaveError_WhenCreateAltTextTooLong()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = new string('A', 201), // 201 characters
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.AltText)
            .WithErrorMessage("Alt text cannot exceed 200 characters.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateBothProductAndOrderIdsSet()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 1,
            CustomizedOrderId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c)
            .WithErrorMessage("Image must be associated with either a product or a customized order, but not both.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateNeitherProductNorOrderIdsSet()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object
            // No ProductId or CustomizedOrderId
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c)
            .WithErrorMessage("Image must be associated with either a product or a customized order, but not both.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateProductIdZero()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            ProductId = 0
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ProductId)
            .WithErrorMessage("Product ID must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenCreateCustomizedOrderIdZero()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            CustomizedOrderId = 0
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.CustomizedOrderId)
            .WithErrorMessage("Customized order ID must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRequestIsValidWithProduct()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Valid product image",
            IsPrimary = true,
            ProductId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenCreateRequestIsValidWithCustomizedOrder()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(1024);
        mockFile.Setup(f => f.FileName).Returns("image.jpg");

        var model = new CreateProductImageRequest
        {
            ImageFile = mockFile.Object,
            AltText = "Valid order image",
            IsPrimary = false,
            CustomizedOrderId = 1
        };
        var result = _createValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsZero()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 0,
            AltText = "Updated text"
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Product image ID must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateIdIsNegative()
    {
        var model = new UpdateProductImageRequest
        {
            Id = -1,
            AltText = "Updated text"
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Product image ID must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_WhenUpdateBothProductAndOrderIdsSet()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 1,
            ProductId = 1,
            CustomizedOrderId = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c)
            .WithErrorMessage("Image must be associated with either a product or a customized order, but not both.");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateImageFileIsNull()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 1,
            ImageFile = null,
            AltText = "Updated text",
            ProductId = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(c => c.ImageFile);
    }

    [Fact]
    public void Should_HaveError_WhenUpdateImageFileInvalid()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(0);

        var model = new UpdateProductImageRequest
        {
            Id = 1,
            ImageFile = mockFile.Object,
            ProductId = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(c => c.ImageFile)
            .WithErrorMessage("Image file cannot be empty");
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValid()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated alt text",
            IsPrimary = true,
            ProductId = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithCustomizedOrder()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated order image",
            IsPrimary = false,
            CustomizedOrderId = 1
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_WhenUpdateRequestIsValidWithPartialFields()
    {
        var model = new UpdateProductImageRequest
        {
            Id = 1,
            AltText = "Updated text only"
            // Other fields are null
        };
        var result = _updateValidator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}