using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Master
{
    public class TMstBrandDto : BaseDeleteEntity
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string BrandImage { get; set; }
        public string Flag { get; set; }
    }


    public class TMstBrandCreateDto
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Code must be between 2 and 5 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        public string Country { get; set; }
        public IFormFile BrandImage { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }


}
