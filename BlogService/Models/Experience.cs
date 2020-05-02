    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Models
{
    public class Experience
    {
        //[Required]
        //[Column("ExperienceId")]
        //[Display(Name = "ExperienceId")]
        public int Id { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("Level")]
        [Display(Name = "Level")]
        public Int64 Level { get; set; }

        [Required]
        //[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("Company")]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [StringLength(50)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "fromDate")]
        public DateTime fromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "toDate")]
        public DateTime toDate { get; set; }

    }
}
