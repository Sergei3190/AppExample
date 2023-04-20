namespace AppExample.Contract.Subscribers;

public interface ISubscriber
{
    void ShowNotification(object sender, EventArgs e);
}
