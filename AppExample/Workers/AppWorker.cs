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
        var companyList = await _companyService.GetListAsync().ConfigureAwait(false);
        await Console.Out.WriteLineAsync(string.Join("; ", companyList));

        var employeeList = await _employeeService.GetListAsync().ConfigureAwait(false);
        await Console.Out.WriteLineAsync(string.Join("; ", employeeList));

        return true;
    }
}