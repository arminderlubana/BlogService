using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogService.Models
{
    public class Skills
    {
        //[Required]
        //[Column("SkillsId")]
       // [Display(Name = "SkillsId")]
        public int Id { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("Level")]
        [Display(Name = "Level")]
        public Int64 Level { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("Skill")]
        [Display(Name = "Skill")]
        public string Skill { get; set; }

      
    }
}
