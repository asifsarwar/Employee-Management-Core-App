using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("Category")]
    public class DbCategory
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Byte[] Picture { get; set; }
    }
}
