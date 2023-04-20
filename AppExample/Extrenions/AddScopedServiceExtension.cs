using AppExample.Application.Services;
using AppExample.Contract.Services;
using AppExample.DAL.Context;
using AppExample.Workers.Interfeices;
using AppExample.Workers;

using Microsoft.Extensions.DependencyInjection;

namespace AppExample.Extrenions;

public static class AddScopedServiceExtension
{
    public static void AddScopedServiceFactory(this IServiceCollection service)
    {
        ArgumentNullException.ThrowIfNull(nameof(service));

        // Your services
        service.AddScoped<IAppWorker, AppWorker>();
        service.AddScoped<ISaveService<AppExampleDb>, SaveService>();
        service.AddScoped<ICompanyService, CompanyService>();
        service.AddScoped<IEmployeeService, EmployeeService>();
    }
}
