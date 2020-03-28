using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("Region")]
    public class DbRegion
    {
        [Key]
        [Required]
        public int RegionId { get; set; }
        [Required]
        [StringLength(5)]
        public string RegionDescription { get; set; }
    }
}
