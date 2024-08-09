namespace Core.Models.Entities.Tables
{
    public class TMstEmployee : BaseEntity
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
        public DateTime? OutDate { get; set; }

        public TMstGeneral Company { get; set; }
        public TMstGeneral Branch { get; set; }
        public TMstGeneral Division { get; set; }
        public TMstGeneral Department { get; set; }
        public TMstGeneral JobGroup { get; set; }
        public TMstGeneral JobTitle { get; set; }
    }
}
