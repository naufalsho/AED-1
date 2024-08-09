namespace Core.Models.Entities.Views
{
    public class VwUploadAsset
    {
        public Guid Id { get; set; }
        public Guid UploadId { get; set; }
        public string VendorName { get; set; }
        public DateTime PODate { get; set; }
        public string PONumber { get; set; }
        public string DeviceCatName { get; set; }
        public string DeviceTypeName { get; set; }
        public string ProductBrandName { get; set; }
        public string ProductTypeName { get; set; }
        public string SerialNumber { get; set; }
        public string MacAddress { get; set; }
        public DateTime? BASTVendorDate { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
        public bool IsAsset { get; set; }
        public decimal? RentPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsValid { get; set; }
        public string Remarks { get; set; }
    }
}
