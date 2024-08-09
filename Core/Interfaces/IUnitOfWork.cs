using Core.Interfaces.IRepositories.Tables.Master;
using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views.Transaction;
using Core.Interfaces.IRepositories.Tables.Transaction;
using Core.Models.Entities.Views;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        ITAccMenuGroupRepo AccMenuGroup { get; }
        ITAccMenuRepo AccMenu { get; }
        ITAccMRoleMenuRepo AccMRoleMenu { get; }
        ITAccRoleRepo AccRole { get; }
        ITAccUserRepo AccUser { get; }
        ITMstEmployeeRepo MstEmployee { get; }
        ITMstGeneralRepo MstGeneral { get; }
        ITtMstYardAreaRepo MstYardArea { get; }
        IMstComparisonTypeRepo MstComparisonType { get; }

        #region Master
        IMstBrandRepo MstBrand { get; }
        IMstCategoryRepo MstCategory { get; }
        IMstClassRepo MstClass { get; }
        IMstClassValueRepo MstClassValue { get; }
        IMstModelRepo MstModel { get; }
        IMstSpecItemRepo MstSpecItem { get; }

		#endregion


		#region Transaction

		ITrnSpecValuesRepo TrnSpecValues { get; }
		ITrnImplementRepo TrnImplement{ get; }

        #endregion


        IVwAccUserMenuRepo VwAccUserMenu { get; }
        IVwMstAssetRepo VwMstAsset { get; }
        IVwMstEmployeeRepo VwMstEmployee { get; }
        IVwTransDeployDeviceRepo VwTransDeployDevice { get; }
        IVwUploadAssetRepo VwUploadAsset { get; }

        IVwDsDeviceAllocatedRepo VwDsDeviceAllocated { get; }
        IVwDsDeviceEndPeriodRepo VwDsDeviceEndPeriod { get; }
        IVwDsDeviceEndPeriodYearRepo VwDsDeviceEndPeriodYear { get; }
        IVwDsDeviceStockByBrandRepo VwDsDeviceStockByBrand { get; }
        IVwDsDeviceStockByCategoryRepo VwDsDeviceStockByCategory { get; }
        IVwDsDeviceStockByStatusRepo VwDsDeviceStockByStatus { get; }
        IVwDsDeviceYoYRepo VwDsDeviceYoY { get; }

        IVwSpecValueMatriksRepo VwSpecValueMatriks { get; }
        IVwComparisonRepo VwComparison { get; }

        IVwUnitSpecRepo VwUnitSpec { get; }

        Task<int> CompleteAsync();
    }
}
