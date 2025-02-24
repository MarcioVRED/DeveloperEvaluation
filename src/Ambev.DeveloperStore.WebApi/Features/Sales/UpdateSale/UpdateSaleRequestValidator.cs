using Ambev.DeveloperStore.Domain.Enums;
using Ambev.DeveloperStore.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for CreateSaleItemRequest that defines validation rules for Sale Item creation.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules
    /// </remarks>
    public UpdateSaleRequestValidator()
    {

    }
}