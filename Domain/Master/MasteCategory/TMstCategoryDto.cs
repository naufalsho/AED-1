using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;
using Core.Models.Entities.Tables.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Master.MasterCategory
{
    public class TMstCategoryDto : BaseDeleteEntity
    {

        public string Code { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int Type{ get; set; }
        public string DescriptionImage { get; set; }
        public bool? IsActived { get; set; }

        public List<TMstCategoryDetailDto> CategoryDetails { get; set; }  

    }

    public class TMstCategoryCreatedDto
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(25, ErrorMessage = "Code cannot exceed 25 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Tag cannot exceed 100 characters.")]
        public string Tag { get; set; }
        public IFormFile DescriptionImage { get; set; }
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public List<string> BrandCode { get; set; }

    }

    public class TMstCategoryUpdatedDto 
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(25, ErrorMessage = "Code cannot exceed 25 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Tag cannot exceed 100 characters.")]
        public string Tag { get; set; }
        public IFormFile DescriptionImage { get; set; }

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public List<string> BrandCode { get; set; }

    }


    public class TMstCategoryDetailDto
    {
        public int Id { get; set; }
        public string CategoryCode { get;set; }
        public string BrandCode { get;set; }
        public TMstBrandDto Brand { get; set; }
    }




    //public class TMstCategory2CreateDto
    //{
    //    [Required(ErrorMessage = "Code is required.")]
    //    [StringLength(5, MinimumLength = 2, ErrorMessage = "Code must be between 2 and 5 characters.")]
    //    public string Code { get; set; }

    //    [Required(ErrorMessage = "Name is required.")]
    //    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    //    public string Name { get; set; }

    //    public string Country { get; set; }
    //    public bool IsActive { get; set; }
    //    public string CreatedBy { get; set; }
    //}


}
