using AppExample.Contract.Dto;
using AppExample.Domain.Entities.App;

using AutoMapper;

namespace AppExample.Application.AutoMappers;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ReverseMap();
    }
}
