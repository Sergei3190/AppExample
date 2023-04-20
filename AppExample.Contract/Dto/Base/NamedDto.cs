using AppExample.Contract.Dto.Base.Interfaces;

namespace AppExample.Contract.Dto.Base;

public abstract class NamedDto : BaseDto, INamedDto
{
    public string Name { get; set; } = null!;
}