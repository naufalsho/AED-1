using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Entities;
using Core.Models.Entities.Tables.Master;
using Domain.Master.ClassValue;
using Domain.Master.MasterModel;
using Domain.Master.MasterSpecItem;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Transaction.Implement
{
    public class TTrnImplementDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Model Code is required.")]
        public string ModelCode { get; set; }

        [Required(ErrorMessage = "Model Code Attach is required.")]
        public string ModelCodeAttach { get; set; }

        [Required(ErrorMessage = "Class Value Code is required.")]
        public string ClassValueCode { get; set; }

        public bool IsActive { get; set; }
       public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IEnumerable<SelectListItem> SLModelProduct { get; set; }
        public IEnumerable<SelectListItem> SLModelAttachment{ get; set; }
        public IEnumerable<SelectListItem> SLClassValue{ get; set; }
        public IEnumerable<SelectListItem> SLModel { get; set; }
        public TMstModelDto Model { get; set; }
        public TMstModelDto ModelAttach{ get; set; }
        public TMstModelDto ModelProduct{ get; set; }
		public TMstClassValueDto ClassValues { get; set; }
		//public List<TTrnImplemenDetailtDto> ImplementDetail { get; set; }

	}

	public class TTrnImplementCreatedDto
    {
        public int? Id { get; set; }
        public string ModelCode { get; set; }
        public string CreatedBy { get; set; }
        public List<TTrnImplemenDetailtDto> ImplementDetail{ get; set; }

    }

    public class TTrnImplemenDetailtDto
    {
        public string ModelCodeAttach { get; set; }
        public string ClassValueCode { get; set; }
        public List<string> ClassValue { get; set; }
    }

    public class TTrnImplementFilterDto
    {
        public string ModelCode { get; set; }
    }

}
