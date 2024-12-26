using DewanSoftTask.Models;
using Microsoft.EntityFrameworkCore;

public class ReceiptSystemContext : DbContext
{
    public ReceiptSystemContext(DbContextOptions<ReceiptSystemContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().ToTable("Items");
        modelBuilder.Entity<Receipt>().ToTable("Receipts");
        modelBuilder.Entity<ReceiptItem>().ToTable("ReceiptItems");

        
        modelBuilder.Entity<Item>().HasData(
    new Item { Id = 1, Name = "Laptop", Price = 1000m, Balance = 50 },
    new Item { Id = 2, Name = "Smartphone", Price = 500m, Balance = 100 },
    new Item { Id = 3, Name = "Tablet", Price = 300m, Balance = 75 },
    new Item { Id = 4, Name = "Headphones", Price = 100m, Balance = 200 },
    new Item { Id = 5, Name = "Keyboard", Price = 50m, Balance = 150 },
    new Item { Id = 6, Name = "Mouse", Price = 50m, Balance = 0 }
);
    }
}
