using Core.Models.Entities.Tables.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Core.Models.Entities.Tables.Transaction
{
    public class TTrnSpecValues : BaseDeleteEntity
    {
        public int? Id{ get; set; }
        public string ModelCode{ get; set; }
        public string SpecItemCode{ get; set; }

        [NotMapped]
        public string Items { get; set; }

        [NotMapped]
        public string SubItems { get; set; }

        public string Values{ get; set; }

        public virtual TMstModel Model{ get; set; }
        public virtual TMstSpecItem SpecItem{ get; set; }

    }
}
