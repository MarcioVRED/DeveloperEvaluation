using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Domain.Repositories
{
    public interface ISaleRepository
    {
        // Adiciona uma nova venda
        void Add(Sale sale);

        // Atualiza uma venda existente
        void Update(Sale sale);

        // Obtém uma venda pelo ID
        Sale GetById(int saleId);

        // Lista todas as vendas
        List<Sale> GetAll();
    }
}
