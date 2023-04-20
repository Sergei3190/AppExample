namespace AppExample.Contract.Events;

public class EntityChangeEventArgs : EventArgs
{
    public string Message { get; }

    public EntityChangeEventArgs() : this(null!)
    {
        
    }

    public EntityChangeEventArgs(string message)
    {
        Message = message;
    }
}
