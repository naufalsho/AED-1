namespace Core.Models.Entities.Tables
{
    public class TMstGeneral : BaseDeleteEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int? ParentId { get; set; }
        public int? Order { get; set; }
        public string ChartColor { get; set; }

        public virtual TMstGeneral Parent { get; set; }
        public virtual ICollection<TMstGeneral> Childs { get; set; }

        public virtual ICollection<TMstEmployee> EmpCompanies { get; set; }
        public virtual ICollection<TMstEmployee> EmpBranches { get; set; }
        public virtual ICollection<TMstEmployee> EmpDivisions { get; set; }
        public virtual ICollection<TMstEmployee> EmpDepartments { get; set; }
        public virtual ICollection<TMstEmployee> EmpJobGroups { get; set; }
        public virtual ICollection<TMstEmployee> EmpJobTitles { get; set; }

    }
}
