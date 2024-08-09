using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstSpecItemCfg : IEntityTypeConfiguration<TMstSpecItem>
    {
        public void Configure(EntityTypeBuilder<TMstSpecItem> builder)
        {
            builder.ToTable("MstSpecItem");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Items).HasMaxLength(250);
            builder.Property(e => e.SubItems).HasMaxLength(250);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasOne(e => e.Category)
                   .WithMany(c => c.SpecItems)
                   .HasForeignKey(e => e.CategoryCode)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
