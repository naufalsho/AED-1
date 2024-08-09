using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;

namespace Domain.MasterYardArea
{
    public class YardAreaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Area is required.")]
        [StringLength(10, ErrorMessage = "Code Area must not exceed 10 characters.")]
        public string CodeArea { get; set; }

        [Required(ErrorMessage = "LocationName is required.")]
        [StringLength(255, ErrorMessage = "LocationName must not exceed 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "CurrentOccupancy is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "CurrentOccupancy must be greater than or equal to 0.")]
        public int CurrentOccupancy { get; set; }
        public string YardQRCode { get; set; }
        public bool IsActive{get;set;}
        public string CreatedBy{get;set;}

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime CreatedDate{get;set;}

        public string UpdatedBy{get;set;}

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime UpdatedDate{get;set;}
        public bool? IsDelete{get;set;}

        public YardAreaDto()
        {
            IsDelete = false;
        }


    }


    public class YardAreaFilterDto{
        public bool? IsDelete{get;set;}
        public int? Id{get;set;}
    }


}
