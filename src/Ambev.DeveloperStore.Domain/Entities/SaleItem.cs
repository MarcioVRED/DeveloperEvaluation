using Ambev.DeveloperStore.Domain.Common;
using System;

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
        public decimal TotalItemAmount { get; private set; }

        public SaleItem(Guid id, Guid saleId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (quantity > 20)
                throw new ArgumentException("It's not possible to sell above 20 identical items.");

            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.");

            Id = id;
            SaleId = saleId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0;
            IsCancelled = false;
            CalculateTotalItemAmount();
        }

        public SaleItem()
        {

        }

        public void ApplyDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 1)
                throw new ArgumentException("Discount percentage must be between 0 and 1.");

            Discount = Quantity * UnitPrice * percentage;
            CalculateTotalItemAmount();
        }

        public void CancelItem()
        {
            IsCancelled = true;
        }

        private void CalculateTotalItemAmount()
        {
            TotalItemAmount = (UnitPrice * Quantity) - Discount;
        }
    }
}
