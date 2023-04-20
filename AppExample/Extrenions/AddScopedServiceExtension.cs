using AppExample.Application.Notifiers;
using AppExample.Application.Services;
using AppExample.Application.Subscribers;
using AppExample.Application.Workers;
using AppExample.Contract.Events;
using AppExample.Contract.Notifiers;
using AppExample.Contract.Services;
using AppExample.Contract.Subscribers;
using AppExample.Contract.Workers.Interfeices;
using AppExample.DAL.Context;

using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddScopedServiceExtension
{
    public static void AddScopedServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
        service.AddScoped<IWorker, AppWorker>();

        service.AddScoped<ISaveService<AppExampleDb>, SaveService>();

        service.AddScoped<ICompanyService, CompanyService>();
        service.AddScoped<IEmployeeService, EmployeeService>();

        service.AddScoped<IEntityChangeNotifier<EntityChangeEventArgs>, EntityChangeNotifier>();
        service.AddScoped<ISubscriber, AppSubscriber>();
    }
}
