using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TtMstYardAreaCfg : IEntityTypeConfiguration<TtMstYardArea>
    {
        public virtual void Configure(EntityTypeBuilder<TtMstYardArea> builder)
        {
            builder.ToTable($"tMstYardArea");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CodeArea)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Capacity)
                .IsRequired();

            builder.Property(e => e.CurrentOccupancy)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CreatedDate)
                .IsRequired();

            builder.Property(e => e.IsActive);
        }
    }
}
