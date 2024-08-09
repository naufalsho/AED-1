using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TTrnSpecValuesCfg : IEntityTypeConfiguration<TTrnSpecValues>
    {
        public void Configure(EntityTypeBuilder<TTrnSpecValues> builder)
        {
            builder.ToTable("SpecValuesMatriks");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Values).HasMaxLength(250);
            builder.Ignore(e => e.Items);
            builder.Ignore(e => e.SubItems);

            builder.Property(e => e.CreatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasOne(e => e.Model)
                   .WithMany(c => c.SpecValues)
                   .HasForeignKey(e => e.ModelCode)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.SpecItem)
                   .WithMany(c => c.SpecValues)
                   .HasForeignKey(e => e.ModelCode)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
