using Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale.CreateSaleItem;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale
/// </summary>
public class CreateSaleRequest
{
    public DateTime SaleDate { get; set; }
    public required string CustomerName { get; set; }
    public required string BranchName { get; set; }
    public required List<CreateSaleItemRequest> Items { get; set; }

}