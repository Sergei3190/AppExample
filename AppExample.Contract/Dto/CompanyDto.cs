using AppExample.Contract.Dto.Base;

namespace AppExample.Contract.Dto;
public class CompanyDto : NamedDto
{
    public override string ToString() => $"(Id: {Id}) {Name}";
}