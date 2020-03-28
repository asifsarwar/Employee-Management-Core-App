using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeManagementCoreApp.Models.DbModels
{
    [Table("OrderDetail")]
    public class DbOrderDetail
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public Int16 Quantity { get; set; }
        [Required]
        public Single Discount { get; set; }
    }
}
