namespace Domain.AccessRole
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public List<RoleMenuDto> RoleMenus { get; set; }
    }

    public class RoleUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<RoleMenuDto> RoleMenus { get; set; }
    }

    public class RoleMenuDto
    {
        public string GroupName { get; set; }
        public string MenuName { get; set; }
        public int MenuId { get; set; }
        public bool AllowView { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
    }
}
