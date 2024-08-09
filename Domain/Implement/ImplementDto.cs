using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterCategory;
using Domain.Master.MasterModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ZXing.QrCode.Internal;

namespace Domain.Comparison
{
    public class ImplementDto
    {
        public int Id { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string ModelCodeAttach { get; set; }
        public string ModelNameAttach { get; set; }
        public string ClassValueCode { get; set; }
        public string Value { get; set; }
        public string ModelImage{ get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public TMstModelDto  Model {get;set;}
        public List<ImplementDto> ListProduct { get; set; }
        public List<ImplementDto> Implements { get; set; }
        public List<TMstModelDto> ListModel{get;set;}
        public IEnumerable<SelectListItem> SLModelUnit { get; set; }
        public IEnumerable<SelectListItem> SLModelAttachment { get; set; }

    }
}
