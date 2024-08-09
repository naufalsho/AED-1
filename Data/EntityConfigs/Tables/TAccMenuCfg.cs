using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TAccMenuCfg : IEntityTypeConfiguration<TAccMenu>
    {
        public virtual void Configure(EntityTypeBuilder<TAccMenu> builder)
        {
            builder.ToTable($"AccMenu");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Controller).HasMaxLength(100);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);

            builder.HasOne(d => d.MenuGroup).WithMany(p => p.Menus).HasForeignKey(d => d.MenuGroupId);
            builder.HasMany(d => d.RoleMenus).WithOne(p => p.Menu).HasForeignKey(d => d.MenuId);
        }
    }
}
