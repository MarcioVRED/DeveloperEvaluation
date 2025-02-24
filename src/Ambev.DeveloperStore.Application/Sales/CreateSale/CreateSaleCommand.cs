using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public int SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItem> Items { get; set; }

        public CreateSaleCommand(int saleNumber,DateTime saleDate, string customerName, string branchName, List<SaleItem> items)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerName = customerName;
            BranchName = branchName;
            Items = items;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
}
