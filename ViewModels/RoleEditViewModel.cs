using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.ViewModels
{
    public class RoleEditViewModel
    {
        [Required]
        [Display(Name="Id")]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Name")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
