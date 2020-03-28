using EmployeeManagementCoreApp.Helper.Database;
using EmployeeManagementCoreApp.Models.DbModels;
using EmployeeManagementCoreApp.Models.DbModelsRepo.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace EmployeeManagementCoreApp.Models.DbModelsRepo.Implementation
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _context { get; set; }
        public SQLEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public DbEmployee AddEmployee(DbEmployee e)
        {
            _context.DbEmployee.Add(e);
            _context.SaveChanges();
            return e;
        }
        public DbEmployee Delete(int id)
        {
            DbEmployee emp = _context.DbEmployee.FirstOrDefault(x => x.EmployeeID == id);
            if(emp != null)
            {
                _context.DbEmployee.Remove(emp);
                _context.SaveChanges();
            }
            return emp;
        }
        public IEnumerable<DbEmployee> GetAllEmployee()
        {
            IEnumerable<DbEmployee> employees = _context.DbEmployee;
            return employees;
        }
        public DbEmployee GetEmployee(int id)
        {
            DbEmployee e = _context.DbEmployee.FirstOrDefault(x => x.EmployeeID == id);
            return e;
        }
        public DbEmployee Update(DbEmployee employeeChanges)
        {
            //DbEmployee e = _context.DbEmployee.FirstOrDefault(s => s.EmployeeID == employeeChanges.EmployeeID);
            //if(e!= null)
            //{
            //    e.Name = employeeChanges.Name;
            //    e.Email = employeeChanges.Email;
            //    e.Department = employeeChanges.Department;
            //}
            var entity = _context.DbEmployee.Attach(employeeChanges);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeChanges;
        }
    }
}
