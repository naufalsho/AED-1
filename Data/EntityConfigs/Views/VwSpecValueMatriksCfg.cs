using Core;
using Core.Models.Entities.Views;
using Core.Models.Entities.Views.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwSpecValueMatriksCfg : IEntityTypeConfiguration<VwSpecValueMatriks>
    {
        public virtual void Configure(EntityTypeBuilder<VwSpecValueMatriks> builder)
        {
            builder.ToView($"VwSpecValueMatriks");
        }
    }
}
