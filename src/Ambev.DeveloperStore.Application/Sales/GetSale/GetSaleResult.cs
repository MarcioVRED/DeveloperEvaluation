using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class CancelSaleResult
{
    public Guid Id { get; set; }
    public required Customer Customer { get; set; }
    public required Branch Branch { get; set; }
    public required List<SaleItem> Items { get; set; }
}
