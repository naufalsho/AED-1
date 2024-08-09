namespace Core.Models.Entities.Tables
{
    public class TAccUser : BaseDeleteEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual TAccRole Role { get; set; }
    }
}
