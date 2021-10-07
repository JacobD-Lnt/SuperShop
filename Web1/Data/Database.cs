using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Web1
{
  public class Database : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public Database(DbContextOptions<Database> options) : base(options) { }
  }
}
