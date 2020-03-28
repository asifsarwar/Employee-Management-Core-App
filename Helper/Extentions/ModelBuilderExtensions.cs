using EmployeeManagementCoreApp.Helper.Enumerations;
using EmployeeManagementCoreApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementCoreApp.Helper.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEmployee>().HasData(
                new DbEmployee
                {
                    EmployeeID = 1,
                    FirstName = "Mary",
                    LastName = "Magdalene",
                    Email = "mary@gmail.com",
                    Department = Dept.HR
                },
                new DbEmployee
                {
                    EmployeeID = 2,
                    FirstName = "Jhon",
                    LastName = "Hopkins",
                    Email = "jhon@gmail.com",
                    Department = Dept.None
                }
            );
            modelBuilder.Entity<DbCustomerCustomerDemo>().HasKey(c => new { c.CustomerID, c.CustomerTypeID });
            modelBuilder.Entity<DbOrderDetail>().HasKey(c => new { c.OrderID, c.ProductID });
            modelBuilder.Entity<DbEmployeeTerritory>().HasKey(c => new { c.EmployeeID, c.TerritoryID });
        }
    }
}
