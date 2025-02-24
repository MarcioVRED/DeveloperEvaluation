using AutoMapper;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CancelSale;

/// <summary>
/// Profile for mapping between Application and API CancelSale responses
/// </summary>
public class CancelSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CancelSale feature
    /// </summary>
    public CancelSaleProfile()
    {
        {
            CreateMap<Guid, Application.Sales.CancelSale.CancelSaleCommand>()
                .ConstructUsing(id => new Application.Sales.CancelSale.CancelSaleCommand(id));
        }
    }
}
