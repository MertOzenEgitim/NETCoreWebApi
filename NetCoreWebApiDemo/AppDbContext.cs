using Microsoft.EntityFrameworkCore;
using NetCoreWebApiDemo.Models;

namespace NetCoreWebApiDemo
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
