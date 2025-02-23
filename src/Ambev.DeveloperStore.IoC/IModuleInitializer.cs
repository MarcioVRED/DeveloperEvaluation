using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperStore.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
