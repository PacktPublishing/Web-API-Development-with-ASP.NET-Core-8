using ConcurrencyConflictDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyConflictDemo.Data;

public class SampleDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public SampleDbContext(DbContextOptions<SampleDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedProductData();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
}
