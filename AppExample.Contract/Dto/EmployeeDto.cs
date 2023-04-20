using AppExample.Contract.Dto.Base;

namespace AppExample.Contract.Dto;

public class EmployeeDto : BaseDto
{
    public int Age { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }

    public Guid CompanyId { get; set; }

    public override string ToString() => $"(Id: {Id}) {LastName} {FirstName} {MiddleName} - age: {Age}";
}