using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.AccessMenu
{
    public class MenuGroupDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsDirectMenu { get; set; }
        public bool IsActive { get; set; }
    }

    public class MenuDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int MenuGroupId { get; set; }
        public string MenuGroupName { get; set; }
        public IEnumerable<SelectListItem> MenuGroups { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public bool IsActive { get; set; }
    }

    public class MenuUpdateDto
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int MenuGroupId { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public bool IsActive { get; set; }
    }
}
