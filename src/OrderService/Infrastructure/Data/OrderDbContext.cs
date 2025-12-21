using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;

namespace OrderService.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        
        modelBuilder.Entity<Order>().HasData(
            new Order("101", "1", 100.50m) { CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new Order("102", "1", 200.00m) { CreatedAt = DateTime.UtcNow.AddDays(-5) }
        );
    }
}