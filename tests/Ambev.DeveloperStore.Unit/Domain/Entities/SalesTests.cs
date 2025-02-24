using Ambev.DeveloperStore.Domain.Entities;
using Xunit;

namespace DeveloperStore.Tests.Sales
{
    public class SalesTests
    {
        [Fact(DisplayName = "Should create a valid sale")]
        public void Should_Create_Valid_Sale()
        {
            var sale = SalesTestData.GenerateValidSale();

            Assert.NotNull(sale);
            Assert.False(sale.IsCancelled);
            Assert.NotEmpty(sale.Items);
        }

        [Fact(DisplayName = "Should not create a sale with an invalid customer")]
        public void Should_Not_Create_Sale_With_Invalid_Customer()
        {
            var sale = SalesTestData.GenerateSaleWithInvalidCustomer();

            Assert.NotNull(sale);
            Assert.Equal("", sale.CustomerName);
        }

        [Fact(DisplayName = "Should not create a sale with no items")]
        public void Should_Not_Create_Sale_With_No_Items()
        {
            var sale = SalesTestData.GenerateSaleWithNoItems();

            Assert.NotNull(sale);
            Assert.Empty(sale.Items);
        }

        [Fact(DisplayName = "Should cancel a sale")]
        public void Should_Cancel_Sale()
        {
            var sale = SalesTestData.GenerateValidSale();
            sale.CancelSale();

            Assert.True(sale.IsCancelled);
        }

        [Fact(DisplayName = "Should apply discounts correctly")]
        public void Should_Apply_Discounts_Correctly()
        {
            var sale = SalesTestData.GenerateValidSale();
            sale.ApplyDiscounts();

            foreach (var item in sale.Items)
            {
                if (item.Quantity >= 4 && item.Quantity <= 9)
                {
                    Assert.Equal(0.10m * (item.Quantity * item.UnitPrice), item.Discount);
                }
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    Assert.Equal(0.20m * (item.Quantity * item.UnitPrice), item.Discount);
                }
            }
        }

        [Fact(DisplayName = "Should throw exception when customerName is null or empty")]
        public void Should_Throw_Exception_When_CustomerName_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Sale(null, "BranchName", new List<SaleItem>()));
            Assert.Throws<ArgumentException>(() => new Sale(string.Empty, "BranchName", new List<SaleItem>()));
        }

        [Fact(DisplayName = "Should throw exception when branchName is null or empty")]
        public void Should_Throw_Exception_When_BranchName_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Sale("CustomerName", null, new List<SaleItem>()));
            Assert.Throws<ArgumentException>(() => new Sale("CustomerName", string.Empty, new List<SaleItem>()));
        }

        [Fact(DisplayName = "Should throw exception when items list is null")]
        public void Should_Throw_Exception_When_Items_List_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => new Sale("CustomerName", "BranchName", null));
        }

        [Fact(DisplayName = "Should not apply discount when quantity is less than 4")]
        public void Should_Not_Apply_Discount_When_Quantity_Is_Less_Than_4()
        {
            var items = new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product1", 1, 100m),
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product2", 2, 150m)
            };
            var sale = new Sale("CustomerName", "BranchName", items);
            sale.ApplyDiscounts();

            foreach (var item in sale.Items)
            {
                Assert.Equal(0m, item.Discount);
            }
        }

        [Fact(DisplayName = "Should throw exception when productName is null or empty")]
        public void Should_Throw_Exception_When_ProductName_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), null, 1, 100m));
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), string.Empty, 1, 100m));
        }

        [Fact(DisplayName = "Should throw exception when quantity is zero or negative")]
        public void Should_Throw_Exception_When_Quantity_Is_Zero_Or_Negative()
        {
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 0, 100m));
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", -1, 100m));
        }

        [Fact(DisplayName = "Should throw exception when unitPrice is zero or negative")]
        public void Should_Throw_Exception_When_UnitPrice_Is_Zero_Or_Negative()
        {
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 1, 0m));
            Assert.Throws<ArgumentException>(() => new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product", 1, -100m));
        }
    }
}