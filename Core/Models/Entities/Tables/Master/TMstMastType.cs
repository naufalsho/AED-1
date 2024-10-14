namespace Core.Models.Entities.Tables.Master
{
    public class TMstMastType : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TMstModel> Models { get; set; } 

    }
}
