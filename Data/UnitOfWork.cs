using System.Reflection;
using Core.Interfaces;
using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Tables.Master;
using Core.Interfaces.IRepositories.Tables.Transaction;
using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Data.Repositories.Tables;
using Data.Repositories.Tables.Master;
using Data.Repositories.Views;
using log4net;
using Microsoft.EntityFrameworkCore.Storage;


namespace Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);




        private IDbContextTransaction _transaction;




        public ITAccMenuGroupRepo AccMenuGroup { get; private set; }
        public ITAccMenuRepo AccMenu { get; private set; }
        public ITAccMRoleMenuRepo AccMRoleMenu { get; private set; }
        public ITAccRoleRepo AccRole { get; private set; }
        public ITAccUserRepo AccUser { get; private set; }
        public ITMstEmployeeRepo MstEmployee { get; private set; }
        public ITMstGeneralRepo MstGeneral { get; private set; }
        public ITtMstYardAreaRepo MstYardArea { get; private set; }
        public IMstComparisonTypeRepo MstComparisonType { get; private set; }

        #region Master

        public IMstBrandRepo MstBrand { get; private set; }

        public IMstCategoryRepo MstCategory { get; private set; }
        public IMstCategoryDetailRepo MstCategoryDetail { get; private set; }

        public IMstClassRepo MstClass { get; private set; }

        public IMstClassValueRepo MstClassValue { get; private set; }

        public IMstModelRepo MstModel { get; private set; }

        public IMstSpecItemRepo MstSpecItem { get; private set; }
        public IMstCapRepo MstCap { get; private set; }
        public IMstLiftingHeightRepo MstLiftingHeight { get; private set; }
        public IMstMastTypeRepo MstMastType { get; private set; }
        public IMstTireRepo MstTire { get; private set; }


        #endregion

        #region Transaction

        public ITrnSpecValuesRepo TrnSpecValues { get; private set; }
        public ITrnImplementRepo TrnImplement { get; private set; }


        #endregion

        #region View
        public IVwAccUserMenuRepo VwAccUserMenu { get; private set; }
        public IVwMstAssetRepo VwMstAsset { get; private set; }
        public IVwMstEmployeeRepo VwMstEmployee { get; private set; }
        public IVwTransDeployDeviceRepo VwTransDeployDevice { get; private set; }
        public IVwUploadAssetRepo VwUploadAsset { get; private set; }

        public IVwDsDeviceAllocatedRepo VwDsDeviceAllocated { get; }
        public IVwDsDeviceEndPeriodRepo VwDsDeviceEndPeriod { get; }
        public IVwDsDeviceEndPeriodYearRepo VwDsDeviceEndPeriodYear { get; }
        public IVwDsDeviceStockByBrandRepo VwDsDeviceStockByBrand { get; }
        public IVwDsDeviceStockByCategoryRepo VwDsDeviceStockByCategory { get; }
        public IVwDsDeviceStockByStatusRepo VwDsDeviceStockByStatus { get; }
        public IVwDsDeviceYoYRepo VwDsDeviceYoY { get; }
        public IVwSpecValueMatriksRepo VwSpecValueMatriks { get; }
        public IVwUnitSpecRepo VwUnitSpec { get; }
        public IVwComparisonRepo VwComparison { get; }

        #endregion


        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            AccMenuGroup = new TAccMenuGroupRepo(_context);
            AccMenu = new TAccMenuRepo(_context);
            AccMRoleMenu = new TAccMRoleMenuRepo(_context);
            AccRole = new TAccRoleRepo(_context);
            AccUser = new TAccUserRepo(_context);
            MstEmployee = new TMstEmployeeRepo(_context);
            MstGeneral = new TMstGeneralRepo(_context);
            MstYardArea = new TtMstYardAreaRepo(_context);
            MstComparisonType = new TMstComparisonTypeRepo(_context);

            #region Master

            MstBrand = new TMstBrandRepo(_context);

            MstCategory = new TMstCategoryRepo(_context);
            MstCategoryDetail = new TMstCategoryDetailRepo(_context);
            MstClass = new TMstClassRepo(_context);
            MstClassValue = new TMstClassValueRepo(_context);
            MstModel = new TMstModelRepo(_context);
            MstSpecItem = new TMstSpecItemRepo(_context);
            MstCap = new TMstCapRepo(_context);
            MstLiftingHeight = new TMstLiftingHeightRepo(_context);
            MstMastType = new TMstMastTypeRepo(_context);
            MstTire = new TMstTireRepo(_context);


            #endregion

            #region Transaction

            TrnSpecValues = new TTrnSpecValuesRepo(_context);
            TrnImplement = new TTrnImplementRepo(_context);

            #endregion

            VwAccUserMenu = new VwAccUserMenuRepo(_context);
            VwMstAsset = new VwMstAssetRepo(_context);
            VwMstEmployee = new VwMstEmployeeRepo(_context);
            VwTransDeployDevice = new VwTransDeployDeviceRepo(_context);
            VwUploadAsset = new VwUploadAssetRepo(_context);

            VwDsDeviceAllocated = new VwDsDeviceAllocatedRepo(_context);
            VwDsDeviceEndPeriod = new VwDsDeviceEndPeriodRepo(_context);
            VwDsDeviceEndPeriodYear = new VwDsDeviceEndPeriodYearRepo(_context);
            VwDsDeviceStockByBrand = new VwDsDeviceStockByBrandRepo(_context);
            VwDsDeviceStockByCategory = new VwDsDeviceStockByCategoryRepo(_context);
            VwDsDeviceStockByStatus = new VwDsDeviceStockByStatusRepo(_context);
            VwDsDeviceYoY = new VwDsDeviceYoYRepo(_context);
            VwSpecValueMatriks = new VwSpecValueMatriksRepo(_context);
            VwUnitSpec = new VwUnitSpecRepo(_context);
            VwComparison = new VwComparisonRepo(_context);
        }

        // public async Task<int> CompleteAsync()
        // {
        //     return await _context.SaveChangesAsync();
        // }



        public async Task<int> CompleteAsync()
        {
            using (_transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _context.SaveChangesAsync();
                    _logger.Info("Changes saved successfully.");
                    _transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw new Exception("An error occurred while saving changes. Rollback was performed.", ex);
                }
            }
        }


        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
