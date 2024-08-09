using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TAccRoleCfg : IEntityTypeConfiguration<TAccRole>
    {
        public virtual void Configure(EntityTypeBuilder<TAccRole> builder)
        {
            builder.ToTable($"AccRole");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);

            builder.HasMany(d => d.Users).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);
            builder.HasMany(d => d.RoleMenus).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);
        }
    }
}
