using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Application.Sales.UpdateSaleItem;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; }
        
        public UpdateSaleCommand(string saleNumber, DateTime saleDate, string customerName, string branchName, List<UpdateSaleItemCommand> items)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerName = customerName;
            BranchName = branchName;
            Items = items;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new UpdateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}

