using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.MasterGeneral
{
    public class MstGeneralDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
        public string ChartColor { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public IEnumerable<SelectListItem> Parents { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsNeedAbbr { get; set; } = false;
        public bool IsNeedOrder { get; set; } = false;
        public bool IsNeedColor { get; set; } = false;
    }

    public class MstGeneralUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
        public string ChartColor { get; set; }
        public bool IsActive { get; set; }
    }
}
