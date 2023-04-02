using EfCoreRelationshipsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipsDemo.Data;

public class SampleDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public SampleDbContext(DbContextOptions<SampleDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

    // One-to-Many
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Post> Posts => Set<Post>();

    // One-to-One
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Address> Addresses => Set<Address>();

    // Many-to-Many
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Actor> Actors => Set<Actor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedInvoiceData();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
}
