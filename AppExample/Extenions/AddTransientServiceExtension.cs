using AppExample.Application.Services;
using AppExample.Contract.Services;

using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddTransientServiceExtension
{
    public static void AddTransientServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
        service.AddTransient<IDbInitializerService, DbInitializerService>();
    }
}