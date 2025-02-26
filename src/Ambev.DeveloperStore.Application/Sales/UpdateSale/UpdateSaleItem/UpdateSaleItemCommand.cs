using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSaleItem
{
    public class UpdateSaleItemCommand : IRequest<UpdateSaleItemResult>
    {
        public Guid SaleId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public UpdateSaleItemCommand(Guid saleId, string productName, int quantity, decimal unitPrice)
        {
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleItemCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

