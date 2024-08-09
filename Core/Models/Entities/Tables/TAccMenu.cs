namespace Core.Models.Entities.Tables
{
    public class TAccMenu : BaseEntity
    {
        public int Id { get; set; }
        public int MenuGroupId { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }

        public virtual TAccMenuGroup MenuGroup { get; set; }
        public virtual ICollection<TAccMRoleMenu> RoleMenus { get; set; }
    }
}
