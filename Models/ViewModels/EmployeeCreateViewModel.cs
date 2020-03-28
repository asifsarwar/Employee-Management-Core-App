using EmployeeManagementCoreApp.Helper.Enumerations;
using EmployeeManagementCoreApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.Models.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name
        { get { return string.Format("{0} {1}", FirstName, LastName); } set { _ = string.Format("{0} {1}", FirstName, LastName); } }
        public Dept? Department { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
