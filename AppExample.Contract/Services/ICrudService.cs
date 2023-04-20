using AppExample.Contract.Dto.Base;

namespace AppExample.Contract.Services;

public interface ICrudService<T> where T : BaseDto
{
    Task<IEnumerable<T>> GetListAsync();
    Task<T?> GetAsync(Guid id);
    Task<Guid> CreateAsync(T? dto);
    Task<Guid> UpdateAsync(T? dto);
    Task<bool> DeleteAsync(Guid id);
}