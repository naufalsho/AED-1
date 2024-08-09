using Core;
using Core.Models.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwMstEmployeeCfg : IEntityTypeConfiguration<VwMstEmployee>
    {
        public virtual void Configure(EntityTypeBuilder<VwMstEmployee> builder)
        {
            builder.ToView($"VwMstEmployee");

        }
    }
}
