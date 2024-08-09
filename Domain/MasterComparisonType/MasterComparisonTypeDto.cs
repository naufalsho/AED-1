using System.ComponentModel.DataAnnotations;
using Core.Models.Entities;

namespace Domain.MasterComparisonType
{
    public class MasterComparisonTypeDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title must not exceed 255 characters.")]
        public string Title { get; set; }

        public bool IsActive{get;set;}
        public string CreatedBy{get;set;}

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime CreatedDate{get;set;}

        public string UpdatedBy{get;set;}

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Text)]
        public DateTime UpdatedDate{get;set;}

    }
}
