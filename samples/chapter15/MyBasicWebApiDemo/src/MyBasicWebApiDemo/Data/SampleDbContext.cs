using Microsoft.EntityFrameworkCore;

using MyBasicWebApiDemo.Models;

namespace MyBasicWebApiDemo.Data;

public class SampleDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public SampleDbContext(DbContextOptions<SampleDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedData();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
}
