using InvoiceApp.IntegrationTests.Helpers;
using InvoiceApp.WebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceApp.IntegrationTests;
public class CustomIntegrationTestsFixture : WebApplicationFactory<Program>
{
    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=InvoiceIntegrationTestDb;Trusted_Connection=True";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Set up a test database
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<InvoiceDbContext>));
            services.Remove(descriptor);
            services.AddDbContext<InvoiceDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            using var scope = services.BuildServiceProvider().CreateScope();
            var scopeServices = scope.ServiceProvider;
            var dbContext = scopeServices.GetRequiredService<InvoiceDbContext>();
            Utilities.InitializeDatabase(dbContext);
        });

        //builder.ConfigureAppConfiguration((context, config) =>
        //{
        //    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        //});
        //builder.UseEnvironment("Development");
    }
}
