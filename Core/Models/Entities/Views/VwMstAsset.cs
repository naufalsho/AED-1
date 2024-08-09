namespace Core.Models.Entities.Views
{
    public class VwMstAsset
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string AssetCompanyName { get; set; }
        public int? BranchId { get; set; }
        public string AssetBranchName { get; set; }
        public string VendorName { get; set; }
        public DateTime PODate { get; set; }
        public string PONumber { get; set; }
        public string DeviceCatName { get; set; }
        public string DeviceTypeName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public string MacAddress { get; set; }
        public string IoTDeviceToken { get; set; }
        public DateTime? BASTVendorDate { get; set; }
        public Guid? BASTVendorFileId { get; set; }
        public string BASTVendorFileName { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public bool IsAsset { get; set; }
        public decimal? RentPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? StartBillingDate { get; set; }
        public bool IsEndPeriod { get; set; }
        public DateTime? BuyBackDate { get; set; }
        public Guid? BuyBackFileId { get; set; }
        public string BuyBackFileName { get; set; }
        public DateTime? BASTVendorReturnDate { get; set; }
        public Guid? BASTVendorReturnFileId { get; set; }
        public string BASTVendorReturnFileName { get; set; }
        public string AssetStatus { get; set; }
        public string NRP { get; set; }
        public string EmpName { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentName { get; set; }
        public string JobGroupName { get; set; }
        public string JobTitleName { get; set; }
        public string EmpStatus { get; set; }
        public string SapAssetNumber {get;set;}
        public string Notes{get;set;} 
    }
}
