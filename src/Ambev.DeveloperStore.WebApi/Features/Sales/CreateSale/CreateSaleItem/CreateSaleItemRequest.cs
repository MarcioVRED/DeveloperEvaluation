using Ambev.DeveloperStore.Domain.Enums;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale.CreateSaleItem;

/// <summary>
/// Represents a request to create a new Item Sale
/// </summary>
public class CreateSaleItemRequest
{
    public Guid SaleId { get; set; }
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public bool IsCancelled { get; set; }
}