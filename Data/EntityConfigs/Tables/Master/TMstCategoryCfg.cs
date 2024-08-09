using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstCategoryCfg : IEntityTypeConfiguration<TMstCategory>
    {
        public void Configure(EntityTypeBuilder<TMstCategory> builder)
        {
            builder.ToTable("MstCategory");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Tag).HasMaxLength(50);
            builder.Property(e => e.Type);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasMany(e => e.CategoryDetails).WithOne(d => d.Category).HasForeignKey(d => d.CategoryCode).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.SpecItems).WithOne(si => si.Category).HasForeignKey(si => si.CategoryCode).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
