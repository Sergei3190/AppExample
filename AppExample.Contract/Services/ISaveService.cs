namespace AppExample.Contract.Services;

public interface ISaveService<T> where T : class
{
    Task<int> SaveAsync(T context, CancellationToken token = default);
}
