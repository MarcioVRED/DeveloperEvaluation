using Ambev.DeveloperStore.Domain.Common;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public bool IsCancelled { get; private set; }
        public decimal TotalItemAmount => (Quantity * UnitPrice) - Discount;

        public SaleItem(Guid id, Guid saleId, string productName, int quantity, decimal unitPrice)
        {
            Id = id;
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0m;
            IsCancelled = false;
        }

        public void CancelItemSale()
        {
            IsCancelled = true;
            // Log or trigger the event
            // EventPublisher.Publish(new SaleCancelledEvent(this));
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 4 && Quantity <= 9)
            {
                Discount = 0.10m * (Quantity * UnitPrice); 
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = 0.20m * (Quantity * UnitPrice); 
            }
        }
    }
}
