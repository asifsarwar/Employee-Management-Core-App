using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("Supplier")]
    public class DbSupplier
    {
        [Key]
        [Required]
        public int SupplierID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(40)]
        public string ContactName { get; set; }
        [StringLength(30)]
        public string ContactTitle { get; set; }
        [StringLength(30)]
        public string Address { get; set; }
        [StringLength(60)]
        public string City { get; set; }
        [StringLength(15)]
        public string Region { get; set; }
        [StringLength(15)]
        public string PostalCode { get; set; }
        [StringLength(10)]
        public string Country { get; set; }
        [StringLength(15)]
        public string Phone { get; set; }
        [StringLength(24)]
        public string Fax { get; set; }
        [StringLength(24)]
        public string HomePage { get; set; }
    }
}
