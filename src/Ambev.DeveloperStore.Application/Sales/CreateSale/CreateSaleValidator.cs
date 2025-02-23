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
        /// Validation rules include:
        /// - Email: Must be in valid format (using EmailValidator)
        /// - Username: Required, must be between 3 and 50 characters
        /// - Password: Must meet security requirements (using PasswordValidator)
        /// - Phone: Must match international format (+X XXXXXXXXXX)
        /// - Status: Cannot be set to Unknown
        /// - Role: Cannot be set to None
        /// </remarks>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.Branch.).SetValidator(new EmailValidator());
            RuleFor(sale => user.Username).NotEmpty().Length(3, 50);
            RuleFor(sale => user.Password).SetValidator(new PasswordValidator());
            RuleFor(sale => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(sale => user.Status).NotEqual(UserStatus.Unknown);
            RuleFor(sale => user.Role).NotEqual(UserRole.None);



            RuleFor(sale => )
        }
    }
}
