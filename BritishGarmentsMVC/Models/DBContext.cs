using Microsoft.EntityFrameworkCore;

namespace BritishGarmentsMVC.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<SalesDetailWithProductDetail> SalesDetailWithProduct { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesDetailWithProductDetail>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }

        public async Task<List<SalesDetailWithProductDetail>> GetSalesDetailsWithProductDetailAsync()
        {
            // Use a raw SQL query to call the stored procedure and return the result
            return await this.SalesDetailWithProduct
                             .FromSqlRaw("EXEC GetSalesDetailsWithProductDetail")
                             .ToListAsync();
        }
    }
}
