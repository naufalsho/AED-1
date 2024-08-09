using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Models.Entities;
using Core.Models.Entities.Tables.Master;
using Domain.Master.Class;
using Domain.Master.MasterCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Domain.Master.MasterSpecItem
{
    public class TMstSpecItemsDto : BaseDeleteEntity
    {
        public string Code { get; set; }
        public string CategoryCode { get; set; }
        public string Items { get; set; }
        public string SubItems { get; set; } 
        public virtual TMstCategoryDto Category { get; set; }
    }


    public class TMstSpecItemsCreatedDto
    {
        [Required(ErrorMessage = "Code is required")]
        [StringLength(25, ErrorMessage = "Code cannot be longer than 25 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Category Code is required")]
        [StringLength(25, ErrorMessage = "Category Code cannot be longer than 25 characters")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "Items is required")]
        [StringLength(250, ErrorMessage = "Items cannot be longer than 250 characters")]
        public string Items { get; set; }

        [StringLength(250, ErrorMessage = "SubItems cannot be longer than 250 characters")]
        public string SubItems { get; set; }
        public string CreatedBy { get; set; }

        public bool IsActive{ get; set; }
        
        public IEnumerable<SelectListItem> Category { get; set; }
    }


}
