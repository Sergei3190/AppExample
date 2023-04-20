using AppExample.Contract.Dto;

namespace AppExample.Contract.Services;

public interface IEmployeeService : ICrudService<EmployeeDto>
{
}