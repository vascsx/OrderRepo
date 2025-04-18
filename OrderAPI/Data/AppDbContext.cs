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
                entity.Property(p => p.ClienteId).IsRequired();
                entity.Property(p => p.ValorTotal).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.DataCriacao).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}

