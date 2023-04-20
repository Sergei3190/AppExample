using AppExample.DAL.Context;
using AppExample.Workers.Interfeices;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppExample.Workers;

public class AppWorker : IAppWorker
{
    private readonly ILogger<AppWorker> _logger;
    private readonly IDbContextFactory<AppExampleDb> _dbContextFactory;

    public AppWorker(ILogger<AppWorker> logger, IDbContextFactory<AppExampleDb> dbContextFactory)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> RunAsync()
    {
        await Console.Out.WriteLineAsync("Run!");
        return await Task.FromResult(true).ConfigureAwait(false);
    }
}