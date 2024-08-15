namespace Core
{
    public static class ConsSecurity
    {
        public static string AesKey => "$b5YdJYsC#3Vnw4V!g#F";
        public static string AesVector => "1NCF53K5qgYzis/GiJTOeg==";
    }

    public static class HttpClientName
    {
        public static string CommonClient => "CommonClient";
    }

    public static class MasterDataType
    {
        public static string DeviceCat => "DEVICE_CAT";
        public static string DeviceType => "DEVICE_TYPE";
        public static string ProductBrand => "PRODUCT_BRAND";
        public static string ProductType => "PRODUCT_TYPE";
        public static string Vendor => "VENDOR";
        public static string Company => "COMPANY";
        public static string Branch => "BRANCH";
        public static string Division => "DIVISION";
        public static string Department => "DEPARTMENT";
        public static string JobGroup => "JOB_GROUP";
        public static string JobTitle => "JOB_TITLE";
    }
    public static class MasterModelType
    {
        public static string Unit = "Unit";
        public static string Attachment = "Attachment";
	}

    public static class ViewDataType
    {
        public static string ModalType => "ModalType";
        public static string ModalTitle => "ModalTitle";
        public static string Controller => "Controller";
        public static string Action => "Action";
    }

    public static class ActionType
    {
        public static string Edit => "Edit";
        public static string Create => "Create";
        public static string Delete => "Delete";
    }

    public static class ModalType
    {
        public static string Create => "Create";
        public static string Edit => "Edit";
        public static string Delete => "Delete";
        public static string Undeploy => "Undeploy";
    }

    public static class AssetStatus
    {
        public static string AVAILABLE => "AVAILABLE";
        public static string ON_USER => "ON_USER";
        public static string ON_REPAIR => "ON_REPAIR";
        public static string END_PERIOD => "END_PERIOD";
        public static string PURCHASED => "PURCHASED";
        public static string BACK_TO_VENDOR => "BACK_TO_VENDOR";
        public static string ASSET_LOST => "ASSET_LOST";

        public static string GetAssetStatusUI(string status)
        {
            var ret = "";

            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status)
                {
                    case var value when value == AssetStatus.AVAILABLE:
                        ret = "Available";
                        break;
                    case var value when value == AssetStatus.ON_USER:
                        ret = "On User";
                        break;
                    case var value when value == AssetStatus.ON_REPAIR:
                        ret = "On Repair";
                        break;
                    case var value when value == AssetStatus.END_PERIOD:
                        ret = "End of Period";
                        break;
                    case var value when value == AssetStatus.PURCHASED:
                        ret = "Purchased by Employee";
                        break;
                    case var value when value == AssetStatus.BACK_TO_VENDOR:
                        ret = "Back to Vendor";
                        break;
                    case var value when value == AssetStatus.ASSET_LOST:
                        ret = "Asset Lost";
                        break;
                    default:
                        break;
                }
            }

            return ret;
        }
    }

    public static class UploadType
    {
        public static string Asset => "ASSET";
    }

    public static class UploadStatus
    {
        public static string Draft => "DRAFT";
        public static string Approve => "APPROVE";
        public static string Reject => "REJECT";
    }

    public static class ViewPath
    {
        #region Shared
        public static string ModalLayout => "~/Views/Shared/_LayoutModal.cshtml";
        public static string MDGeneralParent => "~/Views/Shared/_ProcessMDGeneralParent.cshtml";
        public static string MDGeneral => "~/Views/Shared/_ProcessMDGeneral.cshtml";
        #endregion

        #region  Home
        public static string Dashboard => "~/Views/Dashboard/Index.cshtml";
        public static string Dashboard2 => "~/Views/Dashboard/Index3.cshtml";
        public static string Dashboard3 => "~/Views/Dashboard/Agcon/Index.cshtml";
        public static string Dashboard4 => "~/Views/Dashboard/MHD/Index.cshtml";
        public static string Dashboard5 => "~/Views/Dashboard/Power/Index.cshtml";

        #endregion

        #region AccessManagement
        public static string AccManagementUser => "~/Views/AccessUser/Process.cshtml";
        public static string AccManagementRole => "~/Views/AccessRole/Process.cshtml";
        public static string AccManagementMenu => "~/Views/AccessMenu/ProcessMenu.cshtml";
        public static string AccManagementMenuGroup => "~/Views/AccessMenu/ProcessGroup.cshtml";
        #endregion

        #region MasterData
        public static string MasterAsset => "~/Views/MasterAsset/Process.cshtml";
        public static string MasterEmployee => "~/Views/MasterEmployee/Process.cshtml";
        public static string MasterYardArea => "~/Views/Master/YardArea/Index.cshtml";
        public static string MasterYardAreaForm => "~/Views/Master/YardArea/Form.cshtml";

        // Comparison Type
        public static string MasterComparisonType => "~/Views/Master/ComparisonType/Index.cshtml";

        // Competitor
        public static string MasterCompetitor => "~/Views/Master/Competitor/Index.cshtml";

        // Product
        public static string MasterProduct => "~/Views/Master/Product/Index.cshtml";

        // Brand
        public static string MasterBrand => "~/Views/Master/Brand/Index.cshtml";
        public static string MasterBrandProcess => "~/Views/Master/Brand/Process.cshtml";


        // Category
        public static string MasterCategory => "~/Views/Master/Category/Index.cshtml";
        public static string MasterCategoryProcess => "~/Views/Master/Category/Process.cshtml";


        // Class
        public static string MasterClass => "~/Views/Master/Class/Index.cshtml";
        public static string MasterClassProcess => "~/Views/Master/Class/Process.cshtml";


        // Class
        public static string MasterModel => "~/Views/Master/Model/Index.cshtml";
        public static string MasterModelProcess => "~/Views/Master/Model/Process.cshtml";


        // Spec Items
        public static string MasterSpecItem => "~/Views/Master/SpecItems/Index.cshtml";
        public static string MasterSpecItemProcess => "~/Views/Master/SpecItems/Process.cshtml";





        #endregion

        #region Transaction
        // public static string TransDeployDevice => "~/Views/TransDeployDevice/Process.cshtml";

        // Spec Values
        public static string SpecValuesConf => "~/Views/Setting/SpecValues/Index.cshtml";
        public static string SpecValuesProcess => "~/Views/Setting/SpecValues/Process.cshtml";

        // Implement
        public static string ImplementConf => "~/Views/Setting/Implement/Index.cshtml";
        public static string ImplementProcess=> "~/Views/Setting/Implement/Process.cshtml";
        public static string ImplementCompability => "~/Views/Transaction/Implement/Index.cshtml";
        public static string ImplementCompabilityDetail => "~/Views/Transaction/Implement/Detail.cshtml";

        // Product Spec
        public static string ProductSpec => "~/Views/Transaction/ProductSpec/Index.cshtml";
        public static string ProductSpecDetail => "~/Views/Transaction/ProductSpec/Detail.cshtml";

        //Comparison
        public static string ComparisonProcess => "~/Views/Comparison/Process.cshtml";


        #endregion

        #region Report
        public static string HistoryDetail => "~/Views/ReportHistorical/Detail.cshtml";
        #endregion

        #region  Setting
        public static string SpecCategorize => "~/Views/Setting/SpecCategorize/Index.cshtml";
        #endregion
    }
}
