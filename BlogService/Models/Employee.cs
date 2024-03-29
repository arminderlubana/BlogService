﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogService.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "email")]
        public string email { get; set; }


        [StringLength(50)]
        [Display(Name = "sex")]
        public string Sex { get; set; }

        public IList<Skills> Skills { get; set; }

        public IList<Experience> Experiences { get; set; }
    }
    //public class Employee
    //{
    //    public int EmployeeId { get; set; }

    //    [Required]
    //    [StringLength(50)]
    //    [Display(Name = "Last Name")]
    //    public string LastName { get; set; }

    //    [Required]
    //    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    //    [Column("FirstName")]
    //    [Display(Name = "First Name")]
    //    public string FirstName { get; set; }

    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //    [Display(Name = "DOB")]
    //    public DateTime DOB { get; set; }

    //    [Required]
    //    [StringLength(50)]
    //    [Display(Name = "UserName")]
    //    public string UserName { get; set; }


    //    [StringLength(50)]
    //    [Display(Name = "Designation")]
    //    public string Designation { get; set; }


    //}
}
