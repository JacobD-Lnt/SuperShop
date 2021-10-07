using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Web2
{
  public class Database : DbContext
  {
    public DbSet<Product> Products { get; set; }
    public Database(DbContextOptions<Database> options) : base(options) { }
  }
}
