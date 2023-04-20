using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddSingletonServiceExtension
{
    public static void AddSingletonServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
    }
}
