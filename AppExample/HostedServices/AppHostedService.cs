using AppExample.Contract.Services;
using AppExample.HostedServices.Settings;
using AppExample.Workers.Interfeices;

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
        private readonly IAppWorker _appWorker;

        private int? _exitCode;

        public AppHostedService(ILogger<AppHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IServiceScopeFactory scopeFactory,
            IOptions<AppHostSettings> options,
            IAppWorker appWorker)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _scopeFactory = scopeFactory;
            _options = options;
            _appWorker = appWorker;
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
                        _logger.LogInformation("Запуск воркера...");

                        await using (var scope = _scopeFactory.CreateAsyncScope())
                        {
                            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerService>();

                            await dbInitializer.InitializeAsync(
                                canRemove: _options.Value.DbRecreate,
                                canAddTestData: _options.Value.DbAddTestData)
                            .ConfigureAwait(false);
                        }

                        await _appWorker.RunAsync().ConfigureAwait(false);

                        await Task.Delay(1000);

                        _exitCode = 0;

                        _logger.LogInformation("Воркер отработал успешно, код: {0}", _exitCode);
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
