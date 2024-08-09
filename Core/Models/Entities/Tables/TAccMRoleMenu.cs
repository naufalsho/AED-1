namespace Core.Models.Entities.Tables
{
    public class TAccMRoleMenu : BaseEntity
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool AllowView { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }

        public virtual TAccRole Role { get; set; }
        public virtual TAccMenu Menu { get; set; }
    }
}
