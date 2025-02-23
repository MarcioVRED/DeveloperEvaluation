using Ambev.DeveloperStore.Application.Users.CreateUser;
using Ambev.DeveloperStore.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperStore.WebApi.Mappings;

public class CreateUserRequestProfile : Profile
{
    public CreateUserRequestProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}