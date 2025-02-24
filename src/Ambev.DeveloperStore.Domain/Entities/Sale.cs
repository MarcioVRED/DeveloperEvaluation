using Ambev.DeveloperStore.Domain.Common;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string CustomerName { get; private set; }
        public string BranchName { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }
        public decimal TotalSaleAmount => Items.Sum(item => item.TotalItemAmount);

        // Modificado para permitir a data de venda opcional
        public Sale(string customerName, string branchName, List<SaleItem> items, DateTime? saleDate = null)
        {
            CustomerName = customerName;
            BranchName = branchName;
            Items = items;
            SaleDate = saleDate ?? DateTime.UtcNow;
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
