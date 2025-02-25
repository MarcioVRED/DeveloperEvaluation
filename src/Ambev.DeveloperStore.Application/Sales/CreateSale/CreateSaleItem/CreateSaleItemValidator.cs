using FluentValidation;

namespace Ambev.DeveloperStore.Application.Sales.CreateSaleItem
{

    /// <summary>
    /// Validator for CreateSaleItemCommand that defines validation rules for Sale creation command.
    /// </summary>
    public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleItemCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules 
        /// </remarks>
        public CreateSaleItemCommandValidator()
        {

        }
    }
}
