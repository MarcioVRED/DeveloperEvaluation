namespace Ambev.DeveloperStore.Domain.Entities
{
    public class SaleItem
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount => (Quantity * UnitPrice) - Discount;

        public SaleItem(int productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0m;
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 4 && Quantity <= 9)
            {
                Discount = 0.10m * (Quantity * UnitPrice); // 10% discount
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = 0.20m * (Quantity * UnitPrice); // 20% discount
            }
        }
    }
}
