using EmployeeManagementCoreApp.Helper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [AllowedDomainValidation(allowedDomain:"gmail.com",ErrorMessage ="Domain must be gmail.com")]
        [Remote(action:"IsEmailInUse",controller:"Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}
