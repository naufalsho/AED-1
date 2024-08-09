using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TTrnImplementCfg : IEntityTypeConfiguration<TTrnImplement>
    {
        public void Configure(EntityTypeBuilder<TTrnImplement> builder)
        {
            builder.ToTable("ImplementMatriks");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasOne(e => e.ClassValues)
               .WithMany(c => c.Implements)
               .HasForeignKey(e => e.ClassValueCode)
               .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(e => e.ModelAttach)
							   .WithMany(c => c.AttachedImplements)
							   .HasForeignKey(e => e.ModelCodeAttach)
							   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(e => e.ModelProduct)
				   .WithMany(c => c.ProductImplements)
				   .HasForeignKey(e => e.ModelCode)
				   .OnDelete(DeleteBehavior.Cascade);


		}
    }
}
