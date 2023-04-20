namespace AppExample.Contract.Dto.Base.Interfaces;

public interface INamedDto : IBaseDto
{
    public string Name { get; set; }
}