namespace AppExample.Contract.Notifiers;

public interface IEntityChangeNotifier<T> where T : EventArgs
{
    event EventHandler<T> OnEntityChange;

    Task<bool> SendNotificationAsync(T letter);
}