using Core.Models.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwComparisonCfg : IEntityTypeConfiguration<VwComparison>
    {
        public void Configure(EntityTypeBuilder<VwComparison> builder)
        {
            builder.ToView("sp_GenerateComparison");
            builder.HasKey(e => e.SpecItemCode);


        }
    }
}
