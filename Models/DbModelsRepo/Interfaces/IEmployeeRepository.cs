using EmployeeManagementCoreApp.Models.DbModels;
using System.Collections.Generic;
namespace EmployeeManagementCoreApp.Models.DbModelsRepo.Interfaces
{
    public interface IEmployeeRepository
    {
        DbEmployee GetEmployee(int id);
        IEnumerable<DbEmployee> GetAllEmployee();
        DbEmployee AddEmployee(DbEmployee e);
        DbEmployee Update(DbEmployee e);
        DbEmployee Delete(int id);
    }
}
