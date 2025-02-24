using Ambev.DeveloperStore.Common.Security;
using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperStore.ORM.Repositories
{
    namespace DeveloperStore.Infrastructure.Data
    {
        public class SaleRepository : ISaleRepository
        {
            private readonly List<Sale> _sales = new();
            private readonly DefaultContext _context;

            /// <summary>
            /// Initializes a new instance of SalesRepository
            /// </summary>
            /// <param name="context">The database context</param>
            public SaleRepository(DefaultContext context)
            {
                _context = context;
            }

            public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken)
            {
                await _context.AddAsync(sale, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return sale;
            }

            public async Task<Sale> UpdateAsync(Guid id, Sale updatedSale, CancellationToken cancellationToken = default)
            {
                var sale = await GetByIdAsync(id, cancellationToken);
                if (sale == null)
                {
                    return null; ;
                }

                _context.Sales.Update(sale);
                await _context.SaveChangesAsync(cancellationToken);

                return sale;
            }

            public async Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default)
            {
                var sale = await GetByIdAsync(id, cancellationToken);
                if (sale == null)
                {
                    return false;
                }

                _context.Sales.Update(sale);
                await _context.SaveChangesAsync(cancellationToken);

                return true;

            }

            public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            {
                return await _context.Sale.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            }

        }
    }

}
