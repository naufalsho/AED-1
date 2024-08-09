using Core.Models.Entities.Tables.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Core.Models.Entities.Tables.Transaction
{
    public class TTrnImplement : BaseDeleteEntity
    {
        public int Id{ get; set; }
        public string ModelCode{ get; set; }
        public string ModelCodeAttach { get; set; }
        public string ClassValueCode{ get; set; }

        public virtual TMstClassValue ClassValues{ get; set; }
        public virtual TMstModel ModelAttach { get; set; }
        public virtual TMstModel ModelProduct { get; set; }


    }
}
