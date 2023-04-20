namespace AppExample.Contract.Services;

public interface IDbInitializerService
{
    Task InitializeAsync(bool canRemove, bool canAddTestData, CancellationToken cancel = default);
}