using AppExample.Contract.Dto;
using AppExample.Domain.Entities.App;

using AutoMapper;

namespace AppExample.Application.AutoMappers;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ReverseMap();
    }
}
