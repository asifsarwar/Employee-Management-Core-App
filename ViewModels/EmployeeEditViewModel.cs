using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.ViewModels
{
    public class EmployeeEditViewModel:EmployeeCreateViewModel
    {
        public int EmployeeID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
