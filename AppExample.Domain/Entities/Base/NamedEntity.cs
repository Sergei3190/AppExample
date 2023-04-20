using AppExample.Domain.Entities.Base.Interfaces;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AppExample.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    public string Name { get; set; } = null!;
}

public static class NamedConfiguraton
{
    public static void MapNamedEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : NamedEntity
    {
        builder.MapEntity();

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
    }
}