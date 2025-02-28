using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Products
    {
        [Key]
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? SupplierId { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string? Status { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Comment { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }
    }
}
