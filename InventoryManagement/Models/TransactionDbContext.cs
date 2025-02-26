using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext>options):base(options) 
        {
            
        }

        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Inventory> Inventories { get; set; }

    }
}
