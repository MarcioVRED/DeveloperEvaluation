using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Domain.Events
{
    public class SaleModifiedEvent
    {
        public Sale Sale { get; }

        public SaleModifiedEvent(Sale sale)
        {
            Sale = sale;
        }
    }
}
