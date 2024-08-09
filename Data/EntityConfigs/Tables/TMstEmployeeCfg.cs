using Core;
using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TMstEmployeeCfg : IEntityTypeConfiguration<TMstEmployee>
    {
        public virtual void Configure(EntityTypeBuilder<TMstEmployee> builder)
        {
            builder.ToTable($"MstEmployee");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NRP).IsRequired().HasMaxLength(25);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Status).IsRequired().HasMaxLength(25);

            builder.Property(e => e.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);

            builder.HasOne(d => d.Company).WithMany(p => p.EmpCompanies).HasForeignKey(d => d.CompanyId);
            builder.HasOne(d => d.Branch).WithMany(p => p.EmpBranches).HasForeignKey(d => d.BranchId);
            builder.HasOne(d => d.Division).WithMany(p => p.EmpDivisions).HasForeignKey(d => d.DivisionId);
            builder.HasOne(d => d.Department).WithMany(p => p.EmpDepartments).HasForeignKey(d => d.DepartmentId);
            builder.HasOne(d => d.JobGroup).WithMany(p => p.EmpJobGroups).HasForeignKey(d => d.JobGroupId);
            builder.HasOne(d => d.JobTitle).WithMany(p => p.EmpJobTitles).HasForeignKey(d => d.JobTitleId);
        }
    }
}
