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
                // Obtendo a venda existente pelo ID
                var sale = await GetByIdAsync(id, cancellationToken);
                if (sale == null)
                {
                    return null; // ou você pode lançar uma exceção, por exemplo: throw new NotFoundException("Sale not found");
                }

                // Atualizando as propriedades da venda com os valores da venda atualizada
                sale.UpdateDetails(updatedSale); // Aqui você poderia criar um método na entidade "Sale" que atualize as informações necessárias

                // Marcando a venda como modificada
                _context.Sales.Update(sale);

                // Salvando as alterações no banco de dados
                await _context.SaveChangesAsync(cancellationToken);

                return sale;
            }

            public async Task<bool> CancelAsync(Guid id, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            {
                return await _context.Sale.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            }

        }
    }

}
