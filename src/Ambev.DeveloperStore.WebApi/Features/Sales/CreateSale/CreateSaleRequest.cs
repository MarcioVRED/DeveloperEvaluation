using Ambev.DeveloperStore.Domain.Enums;
using Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale.CreateSaleItem;
using MediatR;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale
/// </summary>
public class CreateSaleRequest
{
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; }
    public string BranchName { get; set; }
    public List<CreateSaleItemRequest> Items { get; set; }

}