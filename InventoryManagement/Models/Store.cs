using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Store
    {
        [Key]
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string? Status { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Comment { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }

        
    }
}
