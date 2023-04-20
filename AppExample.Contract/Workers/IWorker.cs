namespace AppExample.Contract.Workers.Interfeices
{
    public interface IWorker
    {
        Task<bool> RunAsync();
    }
}
