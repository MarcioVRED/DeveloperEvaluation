using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Domain.Events
{
    public class SaleCreatedEvent
    {
        public Sale Sale { get; }

        public SaleCreatedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}
