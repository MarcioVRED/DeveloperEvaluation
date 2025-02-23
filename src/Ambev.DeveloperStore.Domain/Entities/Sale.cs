using Ambev.DeveloperStore.Domain.Common;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int SaleId { get; private set; }
        public DateTime Date { get; private set; }
        public Customer Customer { get; private set; }
        public Branch Branch { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }
        public decimal TotalAmount => Items.Sum(item => item.TotalAmount);

        public Sale(Customer customer, Branch branch, List<SaleItem> items)
        {
            Customer = customer;
            Branch = branch;
            Items = items;
            Date = DateTime.Now;
            IsCancelled = false;
        }

        public void CancelSale()
        {
            IsCancelled = true;
            // Log or trigger the event
            // EventPublisher.Publish(new SaleCancelledEvent(this));
        }

        public void ApplyDiscounts()
        {
            foreach (var item in Items)
            {
                item.ApplyDiscount();
            }
        }
    }
}
