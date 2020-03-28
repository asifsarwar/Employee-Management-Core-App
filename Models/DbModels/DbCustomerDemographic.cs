using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("CustomerDemographic")]
    public class DbCustomerDemographic
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }
    }
}
