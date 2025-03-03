using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSaleItem;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

}
