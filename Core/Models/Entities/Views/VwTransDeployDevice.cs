using System.Security.Cryptography.X509Certificates;

namespace Core.Models.Entities.Views
{
    public class VwTransDeployDevice
    {
        public Guid Id { get; set; }
        public int EmployeeId { get; set; }
        public string NRP { get; set; }
        public string EmpName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentName { get; set; }
        public string JobGroupName { get; set; }
        public string JobTitleName { get; set; }
        public string EmpStatus { get; set; }
        public int AssetId { get; set; }
        public string DeviceCatName { get; set; }
        public string DeviceTypeName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public string MacAddress { get; set; }
        public string HostName { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string AssetStatus { get; set; }
        public DateTime? BASTCompDate { get; set; }
        public Guid? BASTCompFileId { get; set; }
        public string BASTCompFileName { get; set; }
        public DateTime? BASTVendorDate { get; set; }
        public Guid? BASTVendorFileId { get; set; }
        public string BASTVendorFileName { get; set; }
        public DateTime DeployDate { get; set; }
        public string DeployBy { get; set; }
        public string IoTDeviceToken { get; set; }
        public DateTime? UndeployDate { get; set; }
        public string UndeployBy { get; set; }
        public Guid? ReturnFormFileId { get; set; }
        public string ReturnFormFileName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string SapAssetNumber{get;set;}
        public string Notes{get;set;}
    }
}
