using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;

namespace Domain.Master.Tire
{
    public class TMstTireDto : BaseDeleteEntity
    {

        [Required(ErrorMessage = "Code is required.")]
        [StringLength(10, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }
    }


    public class TMstTireCreatedDto
    {
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }

    public class TMstTireUpdatedDto
    {
        [Required(ErrorMessage = "Code is required.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Code must be between 2 and 5 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }


}
