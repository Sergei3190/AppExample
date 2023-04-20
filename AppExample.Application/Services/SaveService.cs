using AppExample.Contract.Services;
using AppExample.DAL.Context;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Services;

public class SaveService : ISaveService<AppExampleDb>
{
    private readonly ILogger<SaveService> _logger;

    public SaveService(ILogger<SaveService> logger)
    {
        _logger = logger;
    }

    public async Task<int> SaveAsync(AppExampleDb context, CancellationToken token = default)
    {
        try
        {
            return await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка при сохранении данных: {0}", ex);
            throw;
        }
    }
}