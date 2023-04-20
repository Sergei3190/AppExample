using AppExample.Contract.Services;
using AppExample.DAL.Context;
using AppExample.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppExample.Application.Services;

public class DbInitializerService : IDbInitializerService
{
    private readonly IDbContextFactory<AppExampleDb> _dbContextFactory;
    private readonly ILogger<DbInitializerService> _logger;

    public DbInitializerService(
        ILogger<DbInitializerService> logger, IDbContextFactory<AppExampleDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task InitializeAsync(bool canRemove, bool canAddTestData, CancellationToken cancel = default)
    {
        if (canRemove)
            await RemoveAsync(cancel).ConfigureAwait(false);

        await using var _db = await _dbContextFactory.CreateDbContextAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Инициализация БД...");

        _logger.LogInformation("Применение миграций БД...");
        await _db.Database.MigrateAsync(cancel).ConfigureAwait(false);
        _logger.LogInformation("Применение миграций БД выполнено");

        if (canAddTestData)
            await InitializerDataAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Инициализация БД выполнена успешно");
    }

    private async Task<bool> RemoveAsync(CancellationToken cancel = default)
    {
        await using var _db = await _dbContextFactory.CreateDbContextAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Удаление БД...");

        var result = await _db.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("{0}", result ? "Удаление БД выполнено успешно" : "Удаление БД не выполнено");

        return result;
    }

    private async Task InitializerDataAsync(CancellationToken cancel)
    {
        await using var _db = await _dbContextFactory.CreateDbContextAsync(cancel).ConfigureAwait(false);

        _logger.LogInformation("Инициализация БД тестовыми данными...");

        if (await _db.Employees.AnyAsync(cancel).ConfigureAwait(false))
        {
            _logger.LogInformation("Инициализация БД тестовыми данными не требуется");
            return;
        }

        await using var transaction = await _db.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

        await _db.Companies.AddRangeAsync(TestData.Companies, cancel).ConfigureAwait(false);
        await _db.Employees.AddRangeAsync(TestData.Employees, cancel).ConfigureAwait(false);

        await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

        await transaction.CommitAsync().ConfigureAwait(false);

        _logger.LogInformation("Инициализация БД тестовыми данными выполнена успешно");
    }
}