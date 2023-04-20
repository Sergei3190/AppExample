using AppExample.Contract.Events;
using AppExample.Contract.Notifiers;
using AppExample.Contract.Subscribers;

namespace AppExample.Application.Subscribers;

/// <summary>
/// Класс для простой демонстрации событий и делегатов
/// </summary>
public class AppSubscriber : ISubscriber
{
    private delegate void PrintMessage(string message);

    public AppSubscriber(IEntityChangeNotifier<EntityChangeEventArgs> changeNotifier)
    {
        changeNotifier.OnEntityChange += ShowNotification!; 
    }

    public void ShowNotification(object sender, EventArgs e)
    {
        PrintMessage printMessage = Print;   

        if (e is EntityChangeEventArgs eventArgs)
             printMessage(eventArgs.Message);
    }
    private void Print(string message) => Console.WriteLine(message);   
}
