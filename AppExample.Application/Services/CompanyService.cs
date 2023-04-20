using AppExample.Contract.Dto;
using AppExample.Contract.Services;
using AppExample.DAL.Context;
using AppExample.Domain.Entities.App;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppExample.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IDbContextFactory<AppExampleDb> _dbContexFactory;
    private readonly ILogger<CompanyService> _logger;
    private readonly IMapper _mapper;
    private readonly ISaveService<AppExampleDb> _saveService;

    public CompanyService(IDbContextFactory<AppExampleDb> dbContextFactory,
        ILogger<CompanyService> logger,
        IMapper mapper,
        ISaveService<AppExampleDb> saveService)
    {
        _dbContexFactory = dbContextFactory;    
        _logger = logger;
        _mapper = mapper;
        _saveService = saveService;
    }

    public async Task<IEnumerable<CompanyDto>> GetListAsync()
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var entities = await db.Companies
            .AsNoTracking()
            .Skip(0)
            .Take(10)
            .ToArrayAsync()
            .ConfigureAwait(false);

        return entities.Select(_mapper.Map<CompanyDto>);
    }

    public async Task<CompanyDto?> GetAsync(Guid id)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var entity = await db.Companies
            .FirstOrDefaultAsync(e => e.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<CompanyDto>(entity);
    }

    public async Task<Guid> SaveAsync(CompanyDto? dto)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        ArgumentNullException.ThrowIfNull(nameof(dto));

        var isNew = false;

        var company = _mapper.Map<Company>(dto);

        if (company.Id == Guid.Empty)
        {
            await db.AddAsync(company).ConfigureAwait(false);
            isNew = true;
        }
        else
            db.Update(company);

        await _saveService.SaveAsync(db).ConfigureAwait(false);

        _logger.LogInformation("Компания \"{0}\" успешно {1}", company.ToString(), isNew ? "создана" : "изменена");

        return company.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var company = new Company() { Id = id };
        db.Attach(company); 
        db.Remove(company);

        await _saveService.SaveAsync(db).ConfigureAwait(false);   

        _logger.LogInformation("Компания c id = {0} успешно удалена", company.Id); 

        return true;
    }
}