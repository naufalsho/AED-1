namespace Domain.Report
{
    public class ReportRequestDto
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public DateTime? EndPeriod { get; set; }
    }

    public class ReportDeviceDto
    {
        public int AssetId { get; set; }
        public string DeviceTypeName { get; set; }
        public string DeviceCatName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public string AssetStatus { get; set; }
    }

    public class ReportDeviceHistoryDto
    {
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? BASTCompDate { get; set; }
        public DateTime? UndeployDate { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public string NRP { get; set; }
        public string EmpName { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string JobGroupName { get; set; }
        public string JobTitleName { get; set; }
        public string EmpStatus { get; set; }
    }

    public class ReportTransDeviceDto
    {
        public string DeviceCatName { get; set; }
        public string DeviceTypeName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public string NRP { get; set; }
        public string EmpName { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string JobGroupName { get; set; }
        public string JobTitleName { get; set; }
        public string EmpStatus { get; set; }
        public DateTime? BASTCompDate { get; set; }
        public DateTime? PeriodTo { get; set; }
    }
}
