using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.Comparison
{
    public class ComparisonDto
    {
        public string SpecItemCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; }
        public Dictionary<string, string> ModelCode { get; set; }
        public List<TMstCategoryDto> Category { get; set; }
        public IEnumerable<SelectListItem> SLClass { get; set; }

        public ComparisonDto()
        {
            ModelCode = new Dictionary<string, string>();
        }
    }

    public class ComparisonFilterDto
    {
        public string ModelCode { get; set; }
        public string CategoryCode { get; set; }
    }

}
