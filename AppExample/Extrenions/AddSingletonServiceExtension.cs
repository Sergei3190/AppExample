using AppExample.Workers;
using AppExample.Workers.Interfeices;

using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddSingletonServiceExtension
{
    public static void AddSingletonServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
        service.AddSingleton<IAppWorker, AppWorker>();
    }
}
