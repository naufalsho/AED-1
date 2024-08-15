using Core.Models.Entities.Tables.Master;

namespace Core.Models.Entities.Tables.Master
{
    public class TMstCategory : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int Type{ get; set; }
        public string DescriptionImage { get; set; }
        public virtual ICollection<TMstCategoryDetail> CategoryDetails { get; set; }
        public virtual ICollection<TMstSpecItem> SpecItems { get; set; } = new List<TMstSpecItem>();


    }
}
namespace Core.Models.Entities.Tables.Master
{ 
    public class TMstCategoryDetail
    {
        public int Id{get;set;}
        public string CategoryCode { get; set; }
        public string BrandCode { get; set; }
        public TMstCategory Category{ get; set; }
        public TMstBrand Brand { get; set; }
    }

}


