using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("EmployeeTerritory")]
    public class DbEmployeeTerritory
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(20)]
        public string TerritoryID { get; set; }
    }
}
