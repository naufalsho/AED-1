using Core.Models.Entities.Tables.Transaction;

namespace Core.Models.Entities.Tables.Master
{
    public class TMstClassValue : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public virtual TMstModel Model { get; set; }
        public virtual ICollection<TTrnImplement> Implements{ get; set; }
    }
}
