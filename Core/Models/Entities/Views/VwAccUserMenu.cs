namespace Core.Models.Entities.Views
{
    public class VwAccUserMenu
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public int GroupOrder { get; set; }
        public string GroupName { get; set; }
        public string GroupIcon { get; set; }
        public int MenuOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuController { get; set; }
        public bool IsDirectMenu { get; set; }
        public bool AllowView { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool MG_IsActive { get; set; }
        public bool M_IsActive { get; set; }
    }
}
