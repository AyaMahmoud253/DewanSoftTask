using DewanSoftTask.Models;
using Microsoft.EntityFrameworkCore;

public class ReceiptSystemContext : DbContext
{
    public ReceiptSystemContext(DbContextOptions<ReceiptSystemContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ReceiptItem> ReceiptItems { get; set; }
}
