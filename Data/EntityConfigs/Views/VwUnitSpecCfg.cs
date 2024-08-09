using Core;
using Core.Models.Entities.Views;
using Core.Models.Entities.Views.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwUnitSpecCfg : IEntityTypeConfiguration<VwUnitSpec>
    {
        public virtual void Configure(EntityTypeBuilder<VwUnitSpec> builder)
        {
            builder.ToView($"sp_GetUnitSpec");
        }
    }
}
