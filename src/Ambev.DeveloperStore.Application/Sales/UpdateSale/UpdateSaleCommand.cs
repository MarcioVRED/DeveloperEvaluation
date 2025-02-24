using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        public string Customer { get; set; }
        public string Branch { get; set; }
        public List<SaleItem> Items { get; set; }

        public UpdateSaleCommand(string customer, string branch, List<SaleItem> items)
        {
            Customer = customer;
            Branch = branch;
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
}
