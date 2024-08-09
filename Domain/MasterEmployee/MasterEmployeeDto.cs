using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.MasterEmployee
{
    public class MstEmployeeDto
    {
        public int Id { get; set; }
        public string NRP { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<SelectListItem> Branches { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public IEnumerable<SelectListItem> Divisions { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }
        public int? JobGroupId { get; set; }
        public string JobGroupName { get; set; }
        public IEnumerable<SelectListItem> JobGroups { get; set; }
        public int? JobTitleId { get; set; }
        public string JobTitleName { get; set; }
        public IEnumerable<SelectListItem> JobTitles { get; set; }
        public string Status { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime? OutDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class MstEmployeeUpdateDto
    {
        public int Id { get; set; }
        public string NRP { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? DivisionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobGroupId { get; set; }
        public int? JobTitleId { get; set; }
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime? OutDate { get; set; }
        public bool IsActive { get; set; }
    }
}
