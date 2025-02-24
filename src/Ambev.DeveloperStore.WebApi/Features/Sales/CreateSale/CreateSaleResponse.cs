using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Enums;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public required string CustomerName { get; set; }
    public required string BranchName { get; set; }
    public required List<SaleItem> Items { get; set; }
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }

}
