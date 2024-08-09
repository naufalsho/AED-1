using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.UnitSpec
{
    public class UnitSpecDto
    {
        public string Id { get; set; }
        public string SpecItemCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; }
        public string Values { get; set; }
        public IEnumerable<SelectListItem> SLBrand { get; set; }
        public IEnumerable<SelectListItem> SLModel { get; set; }
        public List<TMstCategoryDto> Category { get; set; }

    }

    public class UnitSpecFilterDto
    {
        public string CategoryCode { get; set; }
        public string ModelCode { get; set; }
    }
}
