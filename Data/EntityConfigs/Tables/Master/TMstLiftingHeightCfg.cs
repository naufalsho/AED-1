﻿using Core.Models.Entities.Tables.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables.Master
{
    internal class TMstLiftingHeightCfg : IEntityTypeConfiguration<TMstLiftingHeight>
    {
        public void Configure(EntityTypeBuilder<TMstLiftingHeight > builder)
        {
            builder.ToTable("MstLiftingHeight");
            builder.HasKey(e => e.Code);

            builder.Property(e => e.Name).HasMaxLength(200);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.DeletedBy).HasMaxLength(100);



        }
    }
}
