using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext>options):base(options) 
        {
            
        }

        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SalesPerformanceDaily> SalesPerformanceDaily { get; set; }
        public DbSet<SalesPerformanceMonthly> SalesPerformanceMonthly { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<CustomerPurchase> CustomerPurchase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>() // Configuring the Inventory entity
                .HasOne(i => i.Store)        // Inventory has *one* related Store
                .WithMany()                  // A Store can have *many* Inventory records
                .HasForeignKey(i => i.StoreId); // The foreign key is StoreId in Inventory

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Products)  
                .WithMany()
                .HasForeignKey(i => i.ProductId);
        }
    }
}
