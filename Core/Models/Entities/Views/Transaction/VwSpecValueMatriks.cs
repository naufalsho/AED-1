using Core.Models.Entities.Tables.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Entities.Views.Transaction
{
    public class VwSpecValueMatriks
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string ModelCode { get; set; }
        public string SpecItemCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; }
        public string Values { get; set; }
    }
}
