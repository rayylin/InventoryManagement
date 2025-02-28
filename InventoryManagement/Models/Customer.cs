using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class Customer
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Address { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string? Status { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Comment { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }
    }
}
