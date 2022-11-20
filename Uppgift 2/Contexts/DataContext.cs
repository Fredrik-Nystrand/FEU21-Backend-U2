using Uppgift_2.Models;

namespace Uppgift_2.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().ToContainer("Products").HasPartitionKey(x => x.PartitionKey)
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<CategoryEntity>().ToContainer("Categories").HasPartitionKey(x => x.PartitionKey)
                .HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
        }
    }
}
