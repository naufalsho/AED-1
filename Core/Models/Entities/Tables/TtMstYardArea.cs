namespace Core.Models.Entities.Tables
{
    public class TtMstYardArea : BaseDeleteEntity
    {
        public int Id { get; set; }
        public string CodeArea { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public string YardQRCode { get; set; }
    }
}
