namespace Core.Models.Entities.Views
{
    public class VwDsDeviceStockByCategory
    {
        public int Order { get; set; }
        public string DeviceCategory { get; set; }
        public string DeviceStatus { get; set; }
        public int DeviceCount { get; set; }
        public string ChartColor { get; set; }
    }
}
