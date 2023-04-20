using AppExample.Domain.Entities.Public;

using Microsoft.EntityFrameworkCore;

namespace AppExample.DAL.Context;

public class AppExampleDb : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public AppExampleDb()
    {
        
    }

    public AppExampleDb(DbContextOptions<AppExampleDb> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Map(modelBuilder);
    }

    private static void Map(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new Company.Map());
        builder.ApplyConfiguration(new Employee.Map());
    }
}
