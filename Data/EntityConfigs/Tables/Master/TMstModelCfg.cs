using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstModelCfg : IEntityTypeConfiguration<TMstModel>
    {
        public void Configure(EntityTypeBuilder<TMstModel> builder)
        {
            builder.ToTable("MstModel");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Model).HasMaxLength(25);
            builder.Property(e => e.Type).HasMaxLength(25);
            builder.Property(e => e.Distributor).HasMaxLength(100);
            builder.Property(e => e.Country).HasMaxLength(100);
            builder.Property(e => e.ModelImage);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);


            builder.HasOne(e => e.Brand)
                   .WithMany(b => b.Models)
                   .HasForeignKey(e => e.BrandCode)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Classes)
                   .WithMany(c => c.Models)
                   .HasForeignKey(e => e.ClassCode)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ClassValues)
                   .WithOne(cv => cv.Model)
                   .HasForeignKey(cv => cv.Code)
                   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(e => e.AttachedImplements)
				   .WithOne(cv => cv.ModelAttach)
				   .HasForeignKey(cv => cv.ModelCodeAttach)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(e => e.ProductImplements)
				   .WithOne(cv => cv.ModelProduct)
				   .HasForeignKey(cv => cv.ModelCode)
				   .OnDelete(DeleteBehavior.Restrict);




		}
    }
}
