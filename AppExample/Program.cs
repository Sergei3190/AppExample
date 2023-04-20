using System.Diagnostics;

using AppExample.Configuration;
using AppExample.DAL.Context;
using AppExample.Extrenions;
using AppExample.HostedServices;
using AppExample.HostedServices.Settings;
using AppExample.Inizializers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

(IConfiguration config, string environment) congfiguration;

if (!Debugger.IsAttached)
{
    Console.WriteLine("Работаем как служба.");
    congfiguration = Config.GetConfiguration(argsArray: null, isService: true);
}
else
{
    Console.WriteLine("Работаем интерактивно.");
    var configInizializer = new ConfigInitializer();
    congfiguration = configInizializer.GetConfiguration();
}

if (congfiguration.config is null || congfiguration.environment is null)
    return;

var builder = Host.CreateDefaultBuilder()
    .UseEnvironment(congfiguration.environment)
    .ConfigureHostConfiguration(builder =>
    {
        builder.AddConfiguration(congfiguration.config);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContextFactory<AppExampleDb>(opt =>
        {
            opt.UseSqlServer(congfiguration.config.GetConnectionString("SqlServer"));
        });
        services.AddTransientServiceFactory();
        services.AddScopedServiceFactory();
        services.AddSingletonServiceFactory();

        services.AddHostedService<AppHostedService>();
        services.AddOptions<AppHostSettings>().Bind(hostContext.Configuration.GetSection("DbInitialSettings"));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    })
    .Build();

await builder.RunAsync();