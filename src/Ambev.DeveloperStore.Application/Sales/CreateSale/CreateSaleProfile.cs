using AutoMapper;
using Ambev.DeveloperStore.Domain.Entities;

namespace Ambev.DeveloperStore.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<Sale, CreateSaleResult>();
    }
}
