using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;

namespace Ambev.DeveloperStore.ORM.Repositories
{
    namespace DeveloperStore.Infrastructure.Data
    {
        public class SaleRepository : ISaleRepository
        {
            private readonly List<Sale> _sales = new();

            public void Add(Sale sale)
            {
                _sales.Add(sale);
            }

            public void Update(Sale sale)
            {
                var existingSale = _sales.FirstOrDefault(s => s.SaleId == sale.SaleId);
                if (existingSale != null)
                {
                    _sales.Remove(existingSale);
                    _sales.Add(sale);
                }
            }

            public Sale GetById(int saleId)
            {
                return _sales.FirstOrDefault(s => s.SaleId == saleId)!;
            }

            // Lista todas as vendas
            public List<Sale> GetAll()
            {
                return _sales;
            }
        }
    }

}
