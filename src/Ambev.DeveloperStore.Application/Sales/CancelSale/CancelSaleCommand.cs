using MediatR;

namespace Ambev.DeveloperStore.Application.Sales.CancelSale
{
    /// <summary>
    /// Command for retrieving a Sale by their ID
    /// </summary>
    public record CancelSaleCommand : IRequest<CancelSaleResult>
    {
        /// <summary>
        /// The unique identifier of the Sale to retrieve
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        /// <param name="id">The ID of the Sale to Cancel</param>
        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
