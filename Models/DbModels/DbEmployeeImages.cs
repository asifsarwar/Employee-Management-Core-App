using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("EmployeeImages")]
    public class DbEmployeeImages
    {
        [Key]
        public int ImageID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        [Required]
        [StringLength(255)]
        public string FileContentType { get; set; }
        [Required]
        public long FileLength { get; set; }
        [Required]
        public byte[] FileData { get; set; }
    }
}
