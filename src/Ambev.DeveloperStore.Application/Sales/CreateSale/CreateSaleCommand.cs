using Ambev.DeveloperStore.Common.Validation;
using MediatR;
using Ambev.DeveloperStore.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperStore.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public List<SaleItem> Items { get; set; }

        public CreateSaleCommand(Customer customer, Branch branch, List<SaleItem> items)
        {
            Customer = customer;
            Branch = branch;
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
