using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;
using Core.Models.Entities.Tables.Master;
using Domain.Master.Cap;
using Domain.Master.Class;
using Domain.Master.ClassValue;
using Domain.Master.LiftingHeight;
using Domain.Master.MastType;
using Domain.Master.Tire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Domain.Master.MasterModel
{
    public class TMstModelDto : BaseDeleteEntity
    {

        public string Code { get; set; }
        public string Model { get; set; }
        public string Type{ get; set; }
        public string Distributor{ get; set; }
        public string Country { get; set; }

        public string BrandCode { get; set; }
        public string ClassCode { get; set; }
        public string ModelImage { get; set; }
        public string CapCode { get; set; }
        public string LiftingHeightCode { get; set; }
        public string MastTypeCode { get; set; }
        public string TireCode { get; set; }


        public virtual TMstBrandDto Brand { get; set; }
        public virtual TMstClassDto Classes { get; set; }
        public virtual TMstCapDto Cap { get; set; }
        public virtual TMstLiftingHeightDto LiftingHeight { get; set; }
        public virtual TMstMastTypeDto MastType { get; set; }
        public virtual TMstTireDto Tire { get; set; }

        public List<TMstClassValueDto> ClassValue { get; set; }

    }


    public class TMstModelCreatedDto
    {
        public string Code { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string BrandCode { get; set; }
        public string ClassCode { get; set; }
        public string Type {  get; set; }
        public string Distributor {  get; set; }
        public string Country {  get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile ModelImage { get; set; }
        public string CapCode { get; set; }
        public string LiftingHeightCode { get; set; }
        public string MastTypeCode { get; set; }
        public string TireCode { get; set; }


        public IEnumerable<SelectListItem> Brand {  get; set; }
        public IEnumerable<SelectListItem> Class { get; set; }
        public IEnumerable<SelectListItem> Cap { get; set; }
        public IEnumerable<SelectListItem> LiftingHeight { get; set; }
        public IEnumerable<SelectListItem> MastType { get; set; }
        public IEnumerable<SelectListItem> Tire { get; set; }
    }

    public class TMstModelFilterDto
    {
        public string CategoryCode { get; set; }
    }


}
