using Ambev.DeveloperStore.Application.Sales.CreateSale;

public class CreateSaleHandlerTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new CreateSaleCommand
            {
                CustomerName = "Marcio Martins",
                BranchName = "Filial SP",
                SaleDate = DateTime.UtcNow,
                Items = new List<CreateSaleItemCommand>
                {
                    new CreateSaleItemCommand { ProductId = Guid.NewGuid(), ProductName = "Cerveja Pilsen", Quantity = 10, UnitPrice = 5.00 },
                    new CreateSaleItemCommand { ProductId = Guid.NewGuid(), ProductName = "Refrigerante Cola", Quantity = 3, UnitPrice = 7.50 }
                }
            }
        };
    }

}