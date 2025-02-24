using Ambev.DeveloperStore.WebApi.Features.Sales.UpdateSale.UpdateSaleItem;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a sale
/// </summary>
public class UpdateSaleRequest
{
    public int SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public required string CustomerName { get; set; }
    public required string BranchName { get; set; }
    public required List<UpdateSaleItemRequest> Items { get; set; }

}