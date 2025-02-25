 using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;

namespace Ambev.DeveloperStore.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<CreateSaleItemCommand> Items { get; }

        public CreateSaleCommand(DateTime saleDate, string customerName, string branchName, List<CreateSaleItemCommand> items)
        { 
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

