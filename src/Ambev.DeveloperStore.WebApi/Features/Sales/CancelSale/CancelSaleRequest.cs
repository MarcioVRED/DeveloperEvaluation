
namespace Ambev.DeveloperStore.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Represents a request to cancel a sale
/// </summary>
public class CancelSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale to retrieve
    /// </summary>
    public Guid Id { get; set; }

}