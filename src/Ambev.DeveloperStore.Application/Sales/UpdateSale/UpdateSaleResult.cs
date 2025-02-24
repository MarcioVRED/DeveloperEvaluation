namespace Ambev.DeveloperStore.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after successfully updated a sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated sale sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the sale in the system.</value>
    public Guid Id { get; set; }
}
