using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("CustomerCustomerDemo")]
    public class DbCustomerCustomerDemo
    {
        [Required]
        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; }
        [Required]
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }
    }
}
