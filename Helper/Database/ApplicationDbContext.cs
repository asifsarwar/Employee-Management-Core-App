using EmployeeManagementCoreApp.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementCoreApp.Helper.Extentions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeManagementCoreApp.Helper.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        public ApplicationDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Trusted_Connection=true;database= EmployeeDB;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        public DbSet<DbEmployee> DbEmployee { get; set; }
        public DbSet<DbCustomer> DbCustomer { get; set; }
        public DbSet<DbCustomerCustomerDemo> DbCustomerCustomerDemo { get; set; }
        public DbSet<DbCustomerDemographic> DbCustomerDemographic { get; set; }
        public DbSet<DbShipper> DbShipper { get; set; }
        public DbSet<DbTerritory> DbTerritory { get; set; }
        public DbSet<DbEmployeeTerritory> DbEmployeeTerritory { get; set; }
        public DbSet<DbCategory> DbCategory { get; set; }
        public DbSet<DbOrderDetail> DbOrderDetail { get; set; }
        public DbSet<DbOrder> DbOrder { get; set; }
        public DbSet<DbRegion> DbRegion { get; set; }
        public DbSet<DbSupplier> DbSupplier { get; set; }
        public DbSet<DbProduct> DbProduct { get; set; }
        public DbSet<DbEmployeeImages> DbEmployeeImages { get; set; }
    }
}
