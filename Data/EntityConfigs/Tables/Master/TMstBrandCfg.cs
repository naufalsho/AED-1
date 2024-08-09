using Core;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstBrandCfg : IEntityTypeConfiguration<TMstBrand>
    {
        public virtual void Configure(EntityTypeBuilder<TMstBrand> builder)
        {
            builder.ToTable($"MstBrand");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Country).HasMaxLength(250);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            // builder.HasOne(d => d.Parent).WithMany(p => p.Childs).HasForeignKey(d => d.ParentId);
        }
    }
}
