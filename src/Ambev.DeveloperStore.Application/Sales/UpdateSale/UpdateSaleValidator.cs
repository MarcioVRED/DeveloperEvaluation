 using FluentValidation;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSale
{
    /// <summary>
    /// Validator for UpdateSaleCommand that defines validation rules for Sale creation command.
    /// </summary>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules 
        /// </remarks>
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleNumber)
            .Must(SaleNumberValid)
            .WithMessage("SaleNumber must be in the format yyyyMMdd-XXX, where 'XXX' is a sequential number.");

            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("The sale date cannot be in the future.");

            RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Customer name must be between 3 and 100 characters.");

            RuleFor(sale => sale.BranchName)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Branch name must be between 3 and 100 characters.");

            RuleFor(sale => sale.Items)
                .NotEmpty()
                .WithMessage("Sale must have at least one item.");

            RuleForEach(sale => sale.Items)
                .ChildRules(items =>
                {
                    items.RuleFor(item => item.Quantity)
                         .GreaterThan(0)
                         .WithMessage("The product quantity cannot be zero.");
                });
        }
        private bool SaleNumberValid(string saleNumber)
        {
            var parts = saleNumber.Split('-');
            if (parts.Length != 2 || parts[1].Length != 3) return false;
            return DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
