using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public required string Customer { get; set; }
    public required string Branch { get; set; }
    public required List<SaleItem> Items { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }
}
