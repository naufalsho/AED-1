using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Core.Models.Entities.Views;
using Core.Models.Entities.Views.Transaction;
using Data.EntityConfigs.Tables;
using Data.EntityConfigs.Tables.Master;
using Data.EntityConfigs.Views;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }


        public DbSet<TAccMenu> AccMenu { get; set; }
        public DbSet<TAccMenuGroup> AccMenuGroup { get; set; }
        public DbSet<TAccMRoleMenu> AccRoleMenu { get; set; }
        public DbSet<TAccRole> AccRole { get; set; }
        public DbSet<TAccUser> AccUser { get; set; }
        public DbSet<TMstEmployee> MstEmployee { get; set; }
        public DbSet<TMstGeneral> MstGeneral { get; set; }
        public DbSet<TtMstYardArea> MstYardArea { get; set; }
        public DbSet<TMstComparisonType> MstComparisonType { get; set; }

        #region Master
        public DbSet<TMstBrand> MstBrand{ get; set; }
        public DbSet<TMstCategory> MstCategory{ get; set; }
        public DbSet<TMstCategoryDetail> MstCategoryDetail{ get; set; }
        public DbSet<TMstClass> MstClass{ get; set; }
        public DbSet<TMstClassValue> MstClassValue{ get; set; }
        public DbSet<TMstModel> MstModel{ get; set; }
        public DbSet<TMstSpecItem> MstSpecItem{ get; set; }

        #endregion

        #region Transaction

        public DbSet<TTrnSpecValues> TrnSpecValues{ get; set; }
        public DbSet<TTrnImplement> TrnImplement{ get; set; }


        #endregion


        public DbSet<VwAccUserMenu> VwAccUserMenu { get; set; }
        public DbSet<VwMstEmployee> VwMstEmployee { get; set; }
        public DbSet<VwSpecValueMatriks> VwSpecValueMatriks { get; set; }
        public DbSet<VwUnitSpec> VwUnitSpec { get; set; }
        public DbSet<VwComparison> VwComparison { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TAccMenuCfg());
            builder.ApplyConfiguration(new TAccMenuGroupCfg());
            builder.ApplyConfiguration(new TAccMRoleMenuCfg());
            builder.ApplyConfiguration(new TAccRoleCfg());
            builder.ApplyConfiguration(new TAccUserCfg());

            builder.ApplyConfiguration(new TMstEmployeeCfg());
            builder.ApplyConfiguration(new TMstGeneralCfg());
            builder.ApplyConfiguration(new TtMstYardAreaCfg());

            #region Master
            builder.ApplyConfiguration(new TMstBrandCfg());
            builder.ApplyConfiguration(new TMstCategoryCfg());
            builder.ApplyConfiguration(new TMstCategoryDetailCfg());
            builder.ApplyConfiguration(new TMstClassCfg());
            builder.ApplyConfiguration(new TMstClassValueCfg());
            builder.ApplyConfiguration(new TMstModelCfg());
            builder.ApplyConfiguration(new TMstSpecItemCfg());

            #endregion

            #region Transaction
            builder.ApplyConfiguration(new TTrnSpecValuesCfg());
            builder.ApplyConfiguration(new TTrnImplementCfg());

            #endregion

            builder.ApplyConfiguration(new VwAccUserMenuCfg());

            builder.ApplyConfiguration(new VwMstEmployeeCfg());
            builder.ApplyConfiguration(new VwSpecValueMatriksCfg());
            builder.ApplyConfiguration(new VwUnitSpecCfg());
            builder.ApplyConfiguration(new VwComparisonCfg());

        }
    }
}
