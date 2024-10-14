using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstCapCfg : IEntityTypeConfiguration<TMstCap>
    {
        public void Configure(EntityTypeBuilder<TMstCap> builder)
        {
            builder.ToTable("MstCap");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Name).HasMaxLength(200);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);



        }
    }
}
