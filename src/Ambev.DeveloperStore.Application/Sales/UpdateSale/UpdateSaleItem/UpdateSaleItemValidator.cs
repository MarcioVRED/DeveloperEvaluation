using FluentValidation;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSaleItem
{
    /// <summary>
    /// Validator for UpdateSaleCommand that defines validation rules for Sale creation command.
    /// </summary>
    public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules 
        /// </remarks>
        public UpdateSaleItemCommandValidator()
        {

        }
    }
}
