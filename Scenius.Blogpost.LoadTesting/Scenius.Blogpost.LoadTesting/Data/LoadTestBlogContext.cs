using Scenius.Blogpost.LoadTesting.Model;
using Microsoft.EntityFrameworkCore;

namespace Scenius.Blogpost.LoadTesting.Data;

public class LoadTestBlogContext : DbContext
{
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderLine> OrderLines { get; set; }
	public DbSet<Product> Products { get; set; }
	
	public LoadTestBlogContext(DbContextOptions<LoadTestBlogContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseNpgsql("Host=localhost;Database=blogpost;Username=blogpost;Password=blogpost");
}