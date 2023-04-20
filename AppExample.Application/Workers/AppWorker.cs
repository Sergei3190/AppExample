using AppExample.Contract.Dto;
using AppExample.Contract.Events;
using AppExample.Contract.Notifiers;
using AppExample.Contract.Services;
using AppExample.Contract.Subscribers;
using AppExample.Contract.Workers.Interfeices;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Workers;

public class AppWorker : IWorker
{
    private readonly ILogger<AppWorker> _logger;
    private readonly ICompanyService _companyService;
    private readonly IEmployeeService _employeeService;
    private readonly IEntityChangeNotifier<EntityChangeEventArgs> _changeNotifier;

    public AppWorker(ILogger<AppWorker> logger,
        ICompanyService companyService,
        IEmployeeService employeeService,
        IEntityChangeNotifier<EntityChangeEventArgs> changeNotifier,
        ISubscriber _)
    {
        _logger = logger;
        _companyService = companyService;
        _employeeService = employeeService;
        _changeNotifier = changeNotifier;
    }

    public async Task<bool> RunAsync()
    {
        var company = new CompanyDto() { Name = "BMW" };

        var companyId = await _companyService.SaveAsync(company).ConfigureAwait(false);
        company = await _companyService.GetAsync(companyId).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Создана компания \"{company}\"")).ConfigureAwait(false);

        company!.Name = "Moskvich";
        await _companyService.SaveAsync(company).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Изменена компания \"{company}\"")).ConfigureAwait(false);

        var employee = new EmployeeDto()
        {
            LastName = "Криков",
            FirstName = "Марс",
            MiddleName = "Венерович",
            Age = 34,
            CompanyId = companyId
        };

        var employeeId = await _employeeService.SaveAsync(employee).ConfigureAwait(false);
        employee = await _employeeService.GetAsync(employeeId).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Создан сотрудник \"{employee}\"")).ConfigureAwait(false);

        employee!.Age = 100;
        await _employeeService.SaveAsync(employee).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Изменен возраст сотрудника \"{employee}\"")).ConfigureAwait(false);

        await _employeeService.DeleteAsync(employeeId).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Удален сотрудник \"{employee}\"")).ConfigureAwait(false);

        await _companyService.DeleteAsync(companyId).ConfigureAwait(false);
        await _changeNotifier.SendNotificationAsync(new EntityChangeEventArgs($"Удалена компания \"{company}\"")).ConfigureAwait(false);

        var companyList = await _companyService.GetListAsync().ConfigureAwait(false);
        _logger.LogInformation($"Список компаний:{Environment.NewLine}" +
            $"{string.Join(Environment.NewLine, companyList)}");

        var employeeList = await _employeeService.GetListAsync().ConfigureAwait(false);
        _logger.LogInformation($"Список сотрудников:{Environment.NewLine}" +
            $"{string.Join(Environment.NewLine, employeeList)}");

        return true;
    }
}