namespace Core.Models
{
    public class UserClaimModel
    {
        public int Sid { get; set; }
        public string NameIdentifier { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
