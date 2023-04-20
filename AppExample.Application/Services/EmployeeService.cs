using AppExample.Contract.Dto;
using AppExample.Contract.Services;
using AppExample.DAL.Context;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IDbContextFactory<AppExampleDb> _dbContexFactory;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(IDbContextFactory<AppExampleDb> dbContextFactory, ILogger<EmployeeService> logger)
    {
        _dbContexFactory = dbContextFactory;
        _logger = logger;
    }

    public Task<Guid> CreateAsync(EmployeeDto? dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EmployeeDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UpdateAsync(EmployeeDto? dto)
    {
        throw new NotImplementedException();
    }
}