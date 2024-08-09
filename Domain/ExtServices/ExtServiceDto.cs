namespace Domain.ExtServices
{
    public class IoTResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class IoTAddRequest
    {
        public string access_token { get; set; }
        public string device_token { get; set; }
        public string nrp { get; set; }
        public string user_device { get; set; }
        public string assign_branch { get; set; }
        public string mac_address { get; set; }
        public string device_type { get; set; }
        public string product_brand { get; set; }
        public string product_type { get; set; }
        public string job_group { get; set; }
    }

    public class IoTDeleteRequest
    {
        public string access_token { get; set; }
        public string device_token { get; set; }
    }
}
