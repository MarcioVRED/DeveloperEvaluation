using AutoMapper;
using Ambev.DeveloperStore.Application.Users.CreateUser;

namespace Ambev.DeveloperStore.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateUserCommand>();
        CreateMap<CreateUserResult, CreateSaleResponse>();
    }
}
