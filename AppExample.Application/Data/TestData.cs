using AppExample.Domain.Entities.Public;

namespace AppExample.Data;

public static class TestData
{
    public static IEnumerable<Company> Companies { get; } = new List<Company>()
    {
        new () { Name = "Apple" },
        new () { Name = "Google" },
    };

    public static IEnumerable<Employee> Employees { get; } = new List<Employee>()
	{
		new () { Age = 25, LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", Company = Companies!.First() },
		new () { Age = 27, LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович", Company = Companies!.Last() },
		new () { Age = 18, LastName = "Сидоров", FirstName = "Сидор", MiddleName = "Сидорович", Company = Companies!.Last() },
		new () { Age = 19, LastName = "Короваев", FirstName = "Андрей", MiddleName = "Михалыч", Company = Companies!.First() },
		new () { Age = 48, LastName = "Петьков", FirstName = "Ваня", MiddleName = "Сидорович", Company = Companies!.Last() }
	};
}