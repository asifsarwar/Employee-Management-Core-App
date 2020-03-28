using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("Territory")]
    public class DbTerritory
    {
        [Required]
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }
        [Required]
        [StringLength(5)]
        public string TerrritoryDescription { get; set; }
        [Required]
        public int RegionID { get; set; }
    }
}
