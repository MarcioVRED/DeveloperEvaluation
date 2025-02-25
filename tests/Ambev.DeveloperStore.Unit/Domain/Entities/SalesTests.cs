using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Unit.Domain.Entities
{
    public class SalesTests
    {
        [Fact(DisplayName = "Should create a valid sale")]
        public void Should_Create_Valid_Sale()
        {
            var sale = new Sale("Marcio Martins", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 5, 100m)
            }, DateTime.UtcNow);

            Assert.NotNull(sale);
            Assert.False(sale.IsCancelled);
            Assert.NotEmpty(sale.Items);
            Assert.NotNull(sale.SaleNumber);
            Assert.NotEqual(default, sale.SaleDate);
            Assert.Equal(500m, sale.TotalSaleAmount);
        }

        [Fact(DisplayName = "Should throw exception when branchName is null or empty")]
        public void Should_Throw_Exception_When_BranchName_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Sale("Marcio Martins", "", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 5, 100m)
            }));
        }

        [Fact(DisplayName = "Should throw exception when customerName is null or empty")]
        public void Should_Throw_Exception_When_CustomerName_Is_Null_Or_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Sale("", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 5, 100m)
            }));
        }

        [Fact(DisplayName = "Should not create a sale with no items")]
        public void Should_Not_Create_Sale_With_No_Items()
        {
            Assert.Throws<ArgumentException>(() => new Sale("Marcio Martins", "Branch A", new List<SaleItem>()));
        }

        [Fact(DisplayName = "Should cancel a sale")]
        public void Should_Cancel_Sale()
        {
            var sale = new Sale("Marcio Martins", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 5, 100m)
            });

            sale.CancelSale();
            Assert.True(sale.IsCancelled);
        }

        [Fact(DisplayName = "Should apply discounts correctly")]
        public void Should_Apply_Discounts_Correctly()
        {
            var sale = new Sale("Marcio Martins", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 10, 50m)
            });

            sale.ApplyDiscounts();
            Assert.Equal(100m, sale.Items[0].Discount); // 20% de desconto em 10x50
            Assert.Equal(400m, sale.TotalSaleAmount);
        }

        [Fact(DisplayName = "Should not apply discount when quantity is less than 4")]
        public void Should_Not_Apply_Discount_When_Quantity_Is_Less_Than_4()
        {
            var sale = new Sale("Marcio Martins", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 2, 50m)
            });

            sale.ApplyDiscounts();
            Assert.Equal(0m, sale.Items[0].Discount);
            Assert.Equal(100m, sale.TotalSaleAmount);
        }

        [Fact(DisplayName = "Should calculate total sale amount correctly")]
        public void Should_Calculate_Total_Sale_Amount_Correctly()
        {
            var sale = new Sale("Marcio Martins", "Branch A", new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product A", 2, 100m),
                new SaleItem(Guid.NewGuid(), Guid.NewGuid(), "Product B", 3, 50m)
            });

            Assert.Equal(350m, sale.TotalSaleAmount);
        }

        [Fact(DisplayName = "Should throw an exception when sale exceeds the maximum allowed items")]
        public void Should_ThrowException_When_SaleExceedsMaximumAllowedItems()
        {
            var productId = Guid.NewGuid();
            var saleId = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() =>
                new SaleItem(productId, saleId, "Product X", 21, 10.0m)
            );
        }


    }
}
