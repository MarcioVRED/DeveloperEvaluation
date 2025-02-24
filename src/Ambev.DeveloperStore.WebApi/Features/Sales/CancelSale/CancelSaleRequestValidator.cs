using FluentValidation;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Validator for CancelSaleRequest that defines validation rules for Sale Item creation.
/// </summary>
public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CancelSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules
    /// </remarks>
    public CancelSaleRequestValidator()
    {

    }
}