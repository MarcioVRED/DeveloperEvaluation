using System.Collections;
using Ambev.DeveloperStore.Application.Sales.CreateSale;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;

public class CreateSaleHandlerTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new CreateSaleCommand
            (
                saleDate: DateTime.UtcNow.AddDays(-1),
                customerName: "Marcio Martins",
                branchName: "Filial SP",
                items: new List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja Pilsen", 10, 5.00M),
                    new CreateSaleItemCommand(Guid.NewGuid(), "Refrigerante Cola", 3, 7.50M)
                }
            )
        };

        yield return new object[]
        {
            new CreateSaleCommand
            (
                saleDate: DateTime.UtcNow.AddDays(-1),
                customerName: "Carlos Souza",
                branchName: "Filial RJ",
                items: new List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand(Guid.NewGuid(), "Cerveja IPA", 5, 6.50M),
                    new CreateSaleItemCommand(Guid.NewGuid(), "Refrigerante Guaraná", 2, 6.00M)
                }
            )
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
