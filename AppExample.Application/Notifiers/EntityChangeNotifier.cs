using AppExample.Contract.Events;
using AppExample.Contract.Notifiers;

using Microsoft.Extensions.Logging;

namespace AppExample.Application.Notifiers;

public class EntityChangeNotifier : IEntityChangeNotifier<EntityChangeEventArgs>
{
    private readonly ILogger<EntityChangeNotifier> _logger;

    public EntityChangeNotifier(ILogger<EntityChangeNotifier> logger)
    {
        _logger = logger;
    }

    public event EventHandler<EntityChangeEventArgs> OnEntityChange;

    public async Task<bool> SendNotificationAsync(EntityChangeEventArgs letter)
    {
        try
        {
            await Task.Run(() => OnEntityChange?.Invoke(this, letter)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка при отправке уведомления: {0}", ex.Message);
            throw;
        }

        return true;
    }
}