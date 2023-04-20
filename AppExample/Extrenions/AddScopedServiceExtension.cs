using AppExample.Application.Services;
using AppExample.Contract.Services;
using AppExample.DAL.Context;

using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddScopedServiceExtension
{
    public static void AddScopedServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
        service.AddScoped<ISaveService<AppExampleDb>, SaveService>();
    }
}
