using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using System.Text.Json.Serialization;
namespace Core.Models.Entities.Tables.Master
{
    public class TMstSpecItem : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string CategoryCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; }
        public virtual TMstCategory Category { get; set; }
        public virtual ICollection<TTrnSpecValues> SpecValues { get; set; }


    }
}
