using AppExample.Contract.Dto;
using AppExample.Contract.Services;
using AppExample.DAL.Context;
using AppExample.Domain.Entities.App;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IDbContextFactory<AppExampleDb> _dbContexFactory;
    private readonly ILogger<EmployeeService> _logger;
    private readonly IMapper _mapper;
    private readonly ISaveService<AppExampleDb> _saveService;

    public EmployeeService(IDbContextFactory<AppExampleDb> dbContextFactory,
        ILogger<EmployeeService> logger,
        IMapper mapper,
        ISaveService<AppExampleDb> saveService)
    {
        _dbContexFactory = dbContextFactory;
        _logger = logger;
        _mapper = mapper; 
        _saveService = saveService;
    }

    public async Task<IEnumerable<EmployeeDto>> GetListAsync()
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var entities = await db.Employees
            .AsNoTracking()
            .OrderBy(x => x.LastName)
            .Skip(0)
            .Take(10)
            .ToArrayAsync()
            .ConfigureAwait(false);

        return entities.Select(_mapper.Map<EmployeeDto>);
    }

    public async Task<EmployeeDto?> GetAsync(Guid id)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var entity = await db.Employees
            .FirstOrDefaultAsync(e => e.Id == id)
            .ConfigureAwait(false);

        return _mapper.Map<EmployeeDto>(entity);
    }

    public async Task<Guid> SaveAsync(EmployeeDto? dto)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        ArgumentNullException.ThrowIfNull(nameof(dto));

        var isNew = false;

        var employee = _mapper.Map<Employee>(dto);

        if (employee.Id == Guid.Empty)
        {
            await db.AddAsync(employee).ConfigureAwait(false);
            isNew = true;
        }
        else
            db.Update(employee);

        await _saveService.SaveAsync(db).ConfigureAwait(false);

        _logger.LogInformation("Сотрудник \"{0}\" успешно {1}", employee.ToString(), isNew ? "создан" : "изменен");

        return employee.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await using var db = await _dbContexFactory.CreateDbContextAsync().ConfigureAwait(false);

        var employee = new Employee() { Id = id };
        db.Attach(employee);
        db.Remove(employee);

        await _saveService.SaveAsync(db).ConfigureAwait(false);

        _logger.LogInformation("Сотрудник c id = {0} успешно удален", employee.Id);

        return true;
    }
}