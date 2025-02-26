using FluentValidation;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.UpdateSale.UpdateSaleItem;

/// <summary>
/// Validator for UpdateSaleItemRequest that defines validation rules for Sale Item creation.
/// </summary>
public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSalItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules
    /// </remarks>
    public UpdateSaleItemRequestValidator()
    {
        RuleFor(item => item.ProductName)
            .NotEmpty().WithMessage("The product name cannot be null or empty.")
            .Length(3, 100).WithMessage("The product name must be between 3 and 100 characters.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0).WithMessage("The item quantity cannot be zero.");

        RuleFor(item => item.Quantity)
            .GreaterThan(20).WithMessage("The item quantity cannot be greater than 20 items per product.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0).WithMessage("The item unit price must be greater than zero.");

        RuleFor(item => item.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("The discount cannot be negative.");

    }
}