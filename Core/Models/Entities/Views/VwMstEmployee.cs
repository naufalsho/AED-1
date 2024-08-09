namespace Core.Models.Entities.Views
{
    public class VwMstEmployee
    {
        public int Id { get; set; }
        public string NRP { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentName { get; set; }
        public string JobGroupName { get; set; }
        public string JobTitleName { get; set; }
        public DateTime? OutDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
