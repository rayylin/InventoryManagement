using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Transaction
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string TransactionId { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        [DisplayName("Product ID")]
        [Required(ErrorMessage = "Must fill! ")]
        [MaxLength(60, ErrorMessage = "Too long")]
        public string ProductId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Must fill! ")]
        [MaxLength(60, ErrorMessage = "Too long")]
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Comment { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
    }
}
