namespace Core.Models.Entities.Tables
{
    public class TAccRole : BaseDeleteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TAccUser> Users { get; set; }
        public virtual ICollection<TAccMRoleMenu> RoleMenus { get; set; }
    }
}
