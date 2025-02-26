using AutoMapper;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.UpdateSaleItem;

/// <summary>
/// Profile for mapping between Sale entity and UpdateSaleResponse
/// </summary>
public class UpdateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateSale operation
    /// </summary>
    public UpdateSaleItemProfile()
    {
        CreateMap<UpdateSaleItemCommand, Sale>();
        CreateMap<Sale, UpdateSaleItemResult>();
    }
}
