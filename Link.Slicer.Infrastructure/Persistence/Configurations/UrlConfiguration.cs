using Link.Slicer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Link.Slicer.Infrastructure.Persistence.Configurations
{
    /// <summary>
    /// Конфигурация сущности Url
    /// </summary>
    public class UrlConfiguration : BaseEntityConfiguration<Url>
    {
        /// <summary>
        /// Конфигурирует сущность
        /// </summary>
        public override void Configure(EntityTypeBuilder<Url> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Shortening).IsRequired();
            builder.Property(p => p.ExpiresAt).IsRequired(false);
            builder.Property(p => p.Protocol).IsRequired();
            builder.Property(p => p.DomainName).IsRequired();
            builder.Property(p => p.Address).IsRequired(false);
            builder.Property(p => p.Comment).IsRequired(false);

            #region RelationConfigurations
            #endregion
        }
    }
}
