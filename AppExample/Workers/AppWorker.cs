using AppExample.Contract.Dto;
using AppExample.Contract.Services;
using AppExample.Workers.Interfeices;

using Microsoft.Extensions.Logging;

namespace AppExample.Workers;

public class AppWorker : IAppWorker
{
    private readonly ILogger<AppWorker> _logger;
    private readonly ICompanyService _companyService;
    private readonly IEmployeeService _employeeService;

    public AppWorker(ILogger<AppWorker> logger,
        ICompanyService companyService,
        IEmployeeService employeeService
        )
    {
        _logger = logger;
        _companyService = companyService;
        _employeeService = employeeService;
    }

    public async Task<bool> RunAsync()
    {
        var company = new CompanyDto() { Name = "BMW" };

        var companyId = await _companyService.SaveAsync(company).ConfigureAwait(false);
        company = await _companyService.GetAsync(companyId).ConfigureAwait(false);  

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

        await _employeeService.DeleteAsync(employeeId).ConfigureAwait(false);
        await _companyService.DeleteAsync(companyId).ConfigureAwait(false);

        var companyList = await _companyService.GetListAsync().ConfigureAwait(false);
        await Console.Out.WriteLineAsync($"Список компаний:{Environment.NewLine}" +
            $"{string.Join(Environment.NewLine, companyList)}");

        var employeeList = await _employeeService.GetListAsync().ConfigureAwait(false);
        await Console.Out.WriteLineAsync($"Список сотрудников:{Environment.NewLine}" +
            $"{string.Join(Environment.NewLine, employeeList)}");

        return true;
    }
}