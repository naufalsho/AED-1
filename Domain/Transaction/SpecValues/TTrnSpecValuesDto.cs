using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;
using Core.Models.Entities.Tables.Master;
using Domain.Master.MasterModel;
using Domain.Master.MasterSpecItem;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Transaction.SpecValues
{
    public class TTrnSpecValuesDto : BaseDeleteEntity
    {
        public int Id { get; set; }
        public string ModelCode { get; set; }
        public string CategoryCode { get; set; }
        public string SpecItemCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; }
        public string Values { get; set; }

        public virtual TMstModel Model { get; set; }
        public virtual TMstSpecItem SpecItem { get; set; }
        public List<SpecItemValueDto> SpecValues { get; set; }

    }

    public class TTrnSpecValuesCreatedDto
    {
        public int? Id { get; set; }
        public string ModelCode { get; set; }
        public string CategoryCode { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<SelectListItem> Model { get; set; }
        public string CreatedBy { get; set; }
        public List<SpecItemValueCreatedDto> SpecValues { get; set; }

    }


    public class SpecItemValueCreatedDto
    {
        public string Value { get; set; }
        public string SpecItemCode { get; set; }

    }

    public class SpecItemValueDto : SpecItemValueCreatedDto
    {
        public string Items{ get; set; }
        public string SubItems{ get; set; }

    }


    public class TTrnSpecValuesFilterDto
    {
        public string ModelCode{ get; set; }
        public string CategoryCode{ get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<SelectListItem> Model { get; set; }

    }

}
