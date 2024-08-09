using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace Core.Models.Entities.Tables.Master
{
    public class TMstModel : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Distributor { get; set; }
        public string Country { get; set; }

        public string ModelImage { get; set; }


        public string BrandCode { get; set; }
        public string ClassCode { get; set; }



        public virtual TMstBrand Brand { get; set; }
        public virtual TMstClass Classes { get; set; }
        public virtual ICollection<TTrnSpecValues> SpecValues{get;set;}
		public virtual ICollection<TTrnImplement> AttachedImplements { get; set; }
		public virtual ICollection<TTrnImplement> ProductImplements { get; set; }
		public virtual ICollection<TMstClassValue> ClassValues{get;set; }
    }
}
