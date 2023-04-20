using AppExample.Domain.Entities.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppExample.Domain.Entities.Public;

public class Company : NamedEntity
{
    public Company()
    {
        Employees = new HashSet<Employee>();    
    }

    public ICollection<Employee> Employees { get; set; }

    public class Map : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies", "app");

            builder.MapNamedEntity();

            builder.HasMany(c => c.Employees).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId);

            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}