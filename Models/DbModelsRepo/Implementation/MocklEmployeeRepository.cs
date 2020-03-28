using EmployeeManagementCoreApp.Helper.Enumerations;
using EmployeeManagementCoreApp.Models.DbModels;
using EmployeeManagementCoreApp.Models.DbModelsRepo.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace EmployeeManagementCoreApp.Models.DbModelsRepo.Implementation
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<DbEmployee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<DbEmployee>()
            {
              new DbEmployee(){ EmployeeID = 1,Name = "Asif",Email = "asifsarwer@gmail.com",Department = Dept.IT},
              new DbEmployee(){ EmployeeID = 2,Name = "Junaid",Email = "junaid@ymail.com",Department = Dept.HR },
              new DbEmployee(){ EmployeeID = 3,Name = "Omair",Email = "Omair@estonia.com",Department = Dept.Payroll}
            };
        }
        public IEnumerable<DbEmployee> GetAllEmployee()
        {
            return _employeeList;
        }
        public DbEmployee GetEmployee(int id)
        {
            DbEmployee emp = _employeeList.FirstOrDefault(x => x.EmployeeID == id);
            return emp;
        }
        public DbEmployee AddEmployee(DbEmployee e)
        {
            int id = _employeeList.Max(x => x.EmployeeID);
            e.EmployeeID = id+1;
            _employeeList.Add(e);
            return e;
        }
        public DbEmployee Update(DbEmployee e)
        {
            var emp = _employeeList.FirstOrDefault(x => x.EmployeeID == e.EmployeeID);
            if(emp != null)
            {
                emp.Name = e.Name;
                emp.Email = e.Name;
                emp.Department = e.Department;
            }
            return emp;
        }
        public DbEmployee Delete(int id)
        {
            DbEmployee e = _employeeList.Find(x => x.EmployeeID == id);
            _employeeList.Remove(e);
            return e;
        }
    }
}
