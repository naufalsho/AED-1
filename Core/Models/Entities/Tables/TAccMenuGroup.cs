namespace Core.Models.Entities.Tables
{
    public class TAccMenuGroup : BaseEntity
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsDirectMenu { get; set; }

        public virtual ICollection<TAccMenu> Menus { get; set; }
    }
}
