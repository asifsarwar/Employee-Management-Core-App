using EmployeeManagementCoreApp.Models.DbModels;
namespace EmployeeManagementCoreApp.Models.ViewModels
{
    public class HomeDetailsViewModel
    {
        public DbEmployee Employee { get; set; }
        public string PageTitle { get; set; }
    }
}
