using Bogus;
using Ambev.DeveloperStore.Domain.Entities;

namespace DeveloperStore.Tests.Sales
{
    public static class SalesTestData
    {
        private static readonly Faker _faker = new("en");

        public static SaleItem GenerateSaleItem()
        {
            return new SaleItem(
                id: _faker.Random.Guid(),
                saleId: _faker.Random.Guid(),
                productName: _faker.Commerce.ProductName(),
                quantity: _faker.Random.Int(1, 10),
                unitPrice: _faker.Finance.Amount(1, 1000)
            );
        }

        public static List<SaleItem> GenerateSaleItems(int count = 3)
        {
            return new List<SaleItem>(
                new Faker<SaleItem>()
                    .CustomInstantiator(f => new SaleItem(
                        f.Random.Guid(),
                        f.Random.Guid(),
                        f.Commerce.ProductName(),
                        f.Random.Int(1, 10),
                        f.Finance.Amount(1, 1000)
                    ))
                    .Generate(count)
            );
        }

        public static Sale GenerateValidSale()
        {
            return new Sale(
                customerName: _faker.Name.FullName(),
                branchName: _faker.Company.CompanyName(),
                items: GenerateSaleItems()
            );
        }

        public static Sale GenerateSaleWithInvalidCustomer()
        {
            return new Sale(
                customerName: "", // Cliente inválido
                branchName: _faker.Company.CompanyName(),
                items: GenerateSaleItems()
            );
        }

        public static Sale GenerateSaleWithNoItems()
        {
            return new Sale(
                customerName: _faker.Name.FullName(),
                branchName: _faker.Company.CompanyName(),
                items: new List<SaleItem>() // Nenhum item
            );
        }

        public static Sale GenerateSaleWithNegativePrice()
        {
            return new Sale(
                customerName: _faker.Name.FullName(),
                branchName: _faker.Company.CompanyName(),
                items: new List<SaleItem>
                {
                    new SaleItem(
                        _faker.Random.Guid(),
                        _faker.Random.Guid(),
                        _faker.Commerce.ProductName(),
                        1,
                        -10m // Preço negativo
                    )
                }
            );
        }
    }
}
