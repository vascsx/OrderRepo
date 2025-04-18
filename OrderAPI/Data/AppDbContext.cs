using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ClientName).IsRequired(); 
                entity.Property(p => p.Value).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.UpdatedDate).HasDefaultValueSql("GETDATE()");

            });
        }
    }
}

