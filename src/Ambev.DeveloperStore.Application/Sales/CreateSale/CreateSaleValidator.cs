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
            RuleFor(sale => sale.SaleDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("The sale date cannot be in the future.");

            RuleFor(sale => sale.CustomerName)
                .NotNull()
                .WithMessage("Customer name cannot be null.")
                .Length(3, 100)
                .WithMessage("Customer name must be between 3 and 100 characters.");

            RuleFor(sale => sale.BranchName)
                .NotNull()
                .WithMessage("Branch name cannot be null.")
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

        private bool SaleNumberValid(int saleNumber)
        {
            var saleNumberStr = saleNumber.ToString();
            return saleNumberStr.Length == 8 && DateTime.TryParseExact(saleNumberStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
