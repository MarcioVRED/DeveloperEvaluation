using System;
using System.Collections;
using System.Collections.Generic;
using Ambev.DeveloperStore.Application.Sales.CreateSale;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;
using Ambev.DeveloperStore.Domain.Entities; // Add this import

public class CreateSaleHandlerTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new CreateSaleCommand
            (
                saleNumber: 20250112-002,
                saleDate: DateTime.UtcNow,
                customerName: "Marcio Martins",
                branchName: "Filial SP",
                items: new List<SaleItem> // Change CreateSaleItemCommand to SaleItem
                {
                    new CreateSaleItemCommand { SaleId = Guid.NewGuid(), ProductName = "Cerveja Pilsen", Quantity = 10, UnitPrice = 5.00M, Discount = 0M },
                    new CreateSaleItemCommand { SaleId = Guid.NewGuid(), ProductName = "Refrigerante Cola", Quantity = 3, UnitPrice = 7.50M, Discount = 0M }
                }
            )
        };

        yield return new object[]
        {
            new CreateSaleCommand
            (
                saleNumber: 20250112-003,
                saleDate: DateTime.UtcNow,
                customerName: "Carlos Souza",
                branchName: "Filial RJ",
                items: new List<SaleItem> // Change CreateSaleItemCommand to SaleItem
                {
                    new SaleItem { ProductId = Guid.NewGuid(), ProductName = "Cerveja IPA", Quantity = 5, UnitPrice = 6.50M, Discount = 0M },
                    new SaleItem { ProductId = Guid.NewGuid(), ProductName = "Refrigerante Guaraná", Quantity = 2, UnitPrice = 6.00M, Discount = 0M }
                }
            )
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
