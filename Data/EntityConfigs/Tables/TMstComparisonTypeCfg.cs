using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TMstComparisonTypeCfg : IEntityTypeConfiguration<TMstComparisonType>
    {
        public virtual void Configure(EntityTypeBuilder<TMstComparisonType> builder)
        {
            builder.ToTable($"MstComparisonType");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired().HasMaxLength(50);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
        }
    }
}
