namespace Core.Models.Entities
{
    public class BaseDeleteEntity : BaseEntity
    {
        public bool IsDelete { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
