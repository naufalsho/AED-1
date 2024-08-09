namespace Domain.Dashboard
{
    public class DeviceGeneralDto
    {
        public string Parameter { get; set; }
        public string ParameterUI { get; set; }
        public int Count { get; set; }
        public string ChartColor { get; set; }
    }

    public class DeviceStockByStatusDto
    {
        public string DeviceType { get; set; }
        public string DeviceStatus { get; set; }
        public string DeviceStatusUI { get; set; }
        public int DeviceCount { get; set; }
        public string ChartColor { get; set; }
    }

    public class DeviceAllocatedDto
    {
        public string DeviceType { get; set; }
        public string BranchName { get; set; }
        public int DeviceCount { get; set; }
        public string ChartColor { get; set; }
    }

    public class DeviceStockByCategoryDto
    {
        public int Order { get; set; }
        public string DeviceCategory { get; set; }
        public IEnumerable<DeviceGeneralDto> Device { get; set; }
    }

    public class DeviceStockByBrandDto
    {
        public string DeviceType { get; set; }
        public string ProductBrand { get; set; }
        public int DeviceCount { get; set; }
        public string ChartColor { get; set; }
    }

    public class DeviceYoYDto
    {
        public int Year { get; set; }
        public IEnumerable<DeviceGeneralDto> Device { get; set; }
    }

    public class DeviceEndPeriodDto
    {
        public string BranchName { get; set; }
        public IEnumerable<DeviceGeneralDto> Device { get; set; }
    }

    public class DeviceEndPeriodYearDto
    {
        public int Month { get; set; }
        public IEnumerable<DeviceGeneralDto> Device { get; set; }
    }
}
