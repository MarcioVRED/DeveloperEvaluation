using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.GetSale;

/// <summary>
/// Response model for GetSale operation
/// </summary>
public class GetSaleResult
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public required string CustomerName { get; set; }
    public required string BranchName { get; set; }
    public required List<SaleItem> Items { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalSaleAmount { get; set; }
}
