using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TAccMenuGroupCfg : IEntityTypeConfiguration<TAccMenuGroup>
    {
        public virtual void Configure(EntityTypeBuilder<TAccMenuGroup> builder)
        {
            builder.ToTable($"AccMenuGroup");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Icon).IsRequired().HasMaxLength(50);
            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);

            builder.HasMany(d => d.Menus).WithOne(p => p.MenuGroup).HasForeignKey(d => d.MenuGroupId);
        }
    }
}
