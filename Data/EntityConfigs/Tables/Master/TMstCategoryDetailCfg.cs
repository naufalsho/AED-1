using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstCategoryDetailCfg : IEntityTypeConfiguration<TMstCategoryDetail>
    {
        public void Configure(EntityTypeBuilder<TMstCategoryDetail> builder)
        {
            builder.ToTable("MstCategoryDetail");
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Category)
                   .WithMany(c => c.CategoryDetails)
                   .HasForeignKey(e => e.CategoryCode)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Brand)
                   .WithMany(b => b.CategoryDetails)
                   .HasForeignKey(e => e.BrandCode)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
