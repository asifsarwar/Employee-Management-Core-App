using EmployeeManagementCoreApp.Models.DbModels;

namespace EmployeeManagementCoreApp.ViewModels
{
    public class HomeDetailsViewModel
    {
        public DbEmployee Employee { get; set; }
        public string PageTitle { get; set; }
    }
}
