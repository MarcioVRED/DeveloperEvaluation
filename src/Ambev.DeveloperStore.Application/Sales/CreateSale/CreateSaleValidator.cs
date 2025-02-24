using Ambev.DeveloperStore.Domain.Enums;
using Ambev.DeveloperStore.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperStore.Application.Sales.CreateSale
{

    /// <summary>
    /// Validator for CreateUserCommand that defines validation rules for user creation command.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules 
        /// </remarks>
        public CreateSaleCommandValidator()
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

            RuleFor(sale => sale)
                .Must(s => s.Items.Count > 4 || s.Items.All(item => item.Discount == 0))
                .WithMessage("Discount is not allowed when the total number of items is 4 or less.");

            RuleForEach(sale => sale.Items)
                .ChildRules(items =>
                {
                    items.RuleFor(item => item.Quantity)
                         .GreaterThan(0)
                         .WithMessage("The product quantity cannot be zero.");
                });

        }

        private bool SaleNumberValid(int saleNumber)
        {
            var saleNumberStr = saleNumber.ToString();
            return saleNumberStr.Length == 8 && DateTime.TryParseExact(saleNumberStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
