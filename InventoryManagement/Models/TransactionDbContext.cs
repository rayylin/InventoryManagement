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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Store)
                .WithMany()
                .HasForeignKey(i => i.StoreId);
        }
    }
}
