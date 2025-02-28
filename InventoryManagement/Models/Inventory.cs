using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Inventory
    {
        public int ProductId { get; set; }
        public string? StoreId { get; set; }
        public int Quantity { get; set; }
        public int SafetyStock { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string? Status { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Comment { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }

    }
}
