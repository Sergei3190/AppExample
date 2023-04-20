using AppExample.Contract.Dto;
using AppExample.Contract.Services;
using AppExample.DAL.Context;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IDbContextFactory<AppExampleDb> _dbContexFactory;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(IDbContextFactory<AppExampleDb> dbContextFactory, ILogger<CompanyService> logger)
    {
        _dbContexFactory = dbContextFactory;    
        _logger = logger;
    }

    public Task<Guid> CreateAsync(CompanyDto? dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CompanyDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guid> UpdateAsync(CompanyDto? dto)
    {
        throw new NotImplementedException();
    }
}