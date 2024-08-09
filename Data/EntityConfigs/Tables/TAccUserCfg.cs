using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TAccUserCfg : IEntityTypeConfiguration<TAccUser>
    {
        public virtual void Configure(EntityTypeBuilder<TAccUser> builder)
        {
            builder.ToTable($"AccUser");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.UserId).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        }
    }
}
