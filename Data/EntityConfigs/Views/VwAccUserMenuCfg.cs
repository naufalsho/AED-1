using Core;
using Core.Models.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwAccUserMenuCfg : IEntityTypeConfiguration<VwAccUserMenu>
    {
        public virtual void Configure(EntityTypeBuilder<VwAccUserMenu> builder)
        {
            builder.ToView($"VwAccUserMenu");
        }
    }
}
