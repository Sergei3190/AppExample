using AppExample.Domain.Entities.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppExample.Domain.Entities.App;

public class Employee : Entity
{
    public int Age { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public override string ToString() => $"(Id: {Id}) {LastName} {FirstName} {MiddleName} - age: {Age}";

    public class Map : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employess", "app");

            builder.MapEntity();

            builder.Property(e => e.Age).HasColumnName("age");
            builder.Property(e => e.LastName).HasColumnName("last_name");
            builder.Property(e => e.FirstName).HasColumnName("first_name");
            builder.Property(e => e.MiddleName).HasColumnName("middle_name");
            builder.Property(e => e.CompanyId).HasColumnName("company_id");

            builder.HasOne(e => e.Company).WithMany(c =>c.Employees).HasForeignKey(e => e.CompanyId);

            builder.HasIndex(e => new { e.LastName, e.FirstName, e.MiddleName, e.Age }).IsUnique();   
            builder.HasIndex(e => e.FirstName);   
            builder.HasIndex(e => e.MiddleName);
            builder.HasIndex(e => e.Age); 
        }
    }
}