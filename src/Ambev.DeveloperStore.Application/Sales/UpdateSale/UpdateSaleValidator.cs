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
        /// Validation rules include:
        /// - Email: Must be in valid format (using EmailValidator)
        /// - Username: Required, must be between 3 and 50 characters
        /// - Password: Must meet security requirements (using PasswordValidator)
        /// - Phone: Must match international format (+X XXXXXXXXXX)
        /// - Status: Cannot be set to Unknown
        /// - Role: Cannot be set to None
        /// </remarks>
        public UpdateSaleCommandValidator()
        {
            //RuleFor(sale => sale.Branch.).SetValidator(new EmailValidator());
            //RuleFor(sale => user.Username).NotEmpty().Length(3, 50);
            //RuleFor(sale => user.Password).SetValidator(new PasswordValidator());
            //RuleFor(sale => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            //RuleFor(sale => user.Status).NotEqual(UserStatus.Unknown);
            //RuleFor(sale => user.Role).NotEqual(UserRole.None);
        }
    }
}
