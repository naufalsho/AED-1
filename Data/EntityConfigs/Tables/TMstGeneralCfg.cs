using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TMstGeneralCfg : IEntityTypeConfiguration<TMstGeneral>
    {
        public virtual void Configure(EntityTypeBuilder<TMstGeneral> builder)
        {
            builder.ToTable($"MstGeneral");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(10);
            builder.Property(e => e.Type).IsRequired().HasMaxLength(25);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Abbreviation).HasMaxLength(10);
            builder.Property(e => e.ChartColor).HasMaxLength(20);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasOne(d => d.Parent).WithMany(p => p.Childs).HasForeignKey(d => d.ParentId);
        }
    }
}
