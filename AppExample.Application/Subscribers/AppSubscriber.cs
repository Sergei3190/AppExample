using AppExample.Contract.Events;
using AppExample.Contract.Notifiers;
using AppExample.Contract.Subscribers;

namespace AppExample.Application.Subscribers;

public class AppSubscriber : ISubscriber
{
    public AppSubscriber(IEntityChangeNotifier<EntityChangeEventArgs> changeNotifier)
    {
        changeNotifier.OnEntityChange += ShowNotification!;
    }

    public void ShowNotification(object sender, EventArgs e)
    {
        if (e is EntityChangeEventArgs eventArgs)
            Console.WriteLine(eventArgs.Message);
    }
}
