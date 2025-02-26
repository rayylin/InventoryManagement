namespace InventoryManagement.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? SupplierId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }

    }
}
