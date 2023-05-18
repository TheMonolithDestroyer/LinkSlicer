using Link.Slicer.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Slicer.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Базовая fluent конфигурация сущности
    /// </summary>
    /// <typeparam name="T">Сущность</typeparam>
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
            builder.Property(p => p.Id).HasDefaultValueSql("uuid_generate_v4()");
        }
    }
}
