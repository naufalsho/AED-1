using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TAccMRoleMenuCfg : IEntityTypeConfiguration<TAccMRoleMenu>
    {
        public virtual void Configure(EntityTypeBuilder<TAccMRoleMenu> builder)
        {
            builder.ToTable($"AccMRoleMenu");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);

            builder.HasOne(d => d.Role).WithMany(p => p.RoleMenus).HasForeignKey(d => d.RoleId);
            builder.HasOne(d => d.Menu).WithMany(p => p.RoleMenus).HasForeignKey(d => d.MenuId);
        }
    }
}
