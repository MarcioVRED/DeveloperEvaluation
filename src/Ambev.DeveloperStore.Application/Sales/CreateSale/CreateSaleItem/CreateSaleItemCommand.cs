using Ambev.DeveloperStore.Common.Validation;
using MediatR;

namespace Ambev.DeveloperStore.Application.Sales.CreateSaleItem
{
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        public Guid SaleId { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        public CreateSaleItemCommand(Guid saleId, string productName, int quantity,decimal unitPrice, decimal discount)
        {
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;

        }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleItemCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

