using Ambev.DeveloperStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperStore.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string CustomerName { get; private set; }
        public string BranchName { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalSaleAmount { get; private set; }

        public Sale(string customerName, string branchName, List<SaleItem> items, DateTime? saleDate = null)
        {
            if (string.IsNullOrWhiteSpace(branchName))
                throw new ArgumentException("Branch name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name cannot be null or empty.");

            if (items == null || !items.Any())
                throw new ArgumentException("Sale must have at least one item.");

            Id = Guid.NewGuid();
            CustomerName = customerName;
            BranchName = branchName;
            Items = items;
            IsCancelled = false;
            SaleNumber = GenerateSaleNumber();
            SaleDate = saleDate ?? DateTime.UtcNow;
            CalculateTotalSaleAmount();
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }

        public void ApplyDiscounts()
        {
            foreach (var item in Items)
            {
                if (item.Quantity >= 4)
                {
                    item.ApplyDiscount(0.20m); 
                }
            }
            CalculateTotalSaleAmount();
        }

        private void CalculateTotalSaleAmount()
        {
            TotalSaleAmount = Items.Sum(item => (item.UnitPrice * item.Quantity) - item.Discount);
        }

        private string GenerateSaleNumber()
        {
            return $"S-{DateTime.UtcNow:yyyyMMddHHmmss}{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}
