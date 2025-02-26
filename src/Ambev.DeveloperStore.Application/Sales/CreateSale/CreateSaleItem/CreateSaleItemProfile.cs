using AutoMapper;
using Ambev.DeveloperStore.Domain.Entities;
using System.Diagnostics;

namespace Ambev.DeveloperStore.Application.Sales.CreateSaleItem;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleItemResponse
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale operation
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemCommand, SaleItem>();
        CreateMap<SaleItem, CreateSaleItemResult>();
    }
}
