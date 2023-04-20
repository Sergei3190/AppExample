using AppExample.Workers.Interfeices;

using Microsoft.Extensions.Logging;

namespace AppExample.Workers;

public class AppWorker : IAppWorker
{
    private readonly ILogger<AppWorker> _logger;

    public AppWorker(ILogger<AppWorker> logger)
    {
        _logger = logger;
    }

    public async Task<bool> RunAsync()
    {
        await Console.Out.WriteLineAsync("Run!");
        return await Task.FromResult(true).ConfigureAwait(false);
    }
}