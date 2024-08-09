namespace Core.Models.Entities.Tables.Master
{
    public class TMstBrand : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public virtual ICollection<TMstCategoryDetail> CategoryDetails { get; set; } = new List<TMstCategoryDetail>();

        public virtual ICollection<TMstModel> Models { get; set; } = new List<TMstModel>();




    }
}
