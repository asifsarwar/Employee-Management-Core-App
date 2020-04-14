using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementCoreApp.ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public bool IsSelected { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
