using AutoMapper;
using Ambev.DeveloperStore.Application.Sales.CreateSaleItem;
using Ambev.DeveloperStore.WebApi.Features.Sales.CreateSale.CreateSaleItem;

namespace Ambev.DeveloperStore.WebApi.Features.Sales.CreateSaleItem;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
