using System.Diagnostics;

using AppExample.Contract.Services;
using AppExample.Contract.Workers.Interfeices;
using AppExample.HostedServices.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppExample.HostedServices
{
    public sealed class AppHostedService : IHostedService
    {
        private readonly ILogger<AppHostedService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<AppHostSettings> _options;

        private int? _exitCode;

        public AppHostedService(ILogger<AppHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IServiceScopeFactory scopeFactory,
            IOptions<AppHostSettings> options)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _scopeFactory = scopeFactory;
            _options = options;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Запуск приложения...");

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var stopWatch = new Stopwatch();

                        _logger.LogInformation("Запуск воркера...");

                        stopWatch.Start();
                        await using (var scope = _scopeFactory.CreateAsyncScope())
                        {
                            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();

                            await dbInitializer.InitializeAsync(
                                canRemove: _options.Value.DbRecreate,
                                canAddTestData: _options.Value.DbAddTestData)
                            .ConfigureAwait(false);
                        }

                        await using (var scope = _scopeFactory.CreateAsyncScope())
                        {
                            var appWorker = scope.ServiceProvider.GetRequiredService<IWorker>();

                            await appWorker.RunAsync().ConfigureAwait(false);
                        }
                        stopWatch.Stop();

                        await Task.Delay(1000);

                        _exitCode = 0;

                        _logger.LogInformation("Воркер отработал успешно, код: {0}, время выполнения (сек): {1} ", _exitCode, stopWatch.Elapsed.Seconds);
                    }
                    catch (Exception ex)
                    {
                        _exitCode = 1;
                        _logger.LogError(ex, "Необработанная ошибка! код: {0}", _exitCode);
                    }
                    finally
                    {
                        _appLifetime.StopApplication();
                        _logger.LogInformation("Остановка приложения");
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Выход с кодом возврата: {_exitCode}");
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
            return Task.CompletedTask;
        }
    }
}
