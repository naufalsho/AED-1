namespace Core.Models
{
    public class AppSettings
    {
        public string DefaultPassword { get; set; }
        public string FileRepoPath { get; set; }
        public string SchedulerRole { get; set; }
    }

    public class ExtConfigs
    {
        public string IoTAccessToken { get; set; }
        public string IoTUrl { get; set; }
    }

}
