namespace AppExample.Workers.Interfeices
{
    public interface IAppWorker
    {
        Task<bool> RunAsync();
    }
}
