using Azure.Monitor.OpenTelemetry.AspNetCore;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

using MyWebApiDemo.Core.Services;
using MyWebApiDemo.Data;
using MyWebApiDemo.HealthChecks;
using MyWebApiDemo.Models.Validators;
using MyWebApiDemo.OpenTelemetry.Metrics;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

using CustomerService = MyWebApiDemo.Services.CustomerService;
using ProductService = MyWebApiDemo.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Use EF Core health checks
builder.Services.AddHealthChecks().AddDbContextCheck<InvoiceDbContext>("Database", tags: new[] { "database" });

builder.Services.AddHttpClient("JsonPlaceholder", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});

builder.Services.AddHttpClient<CustomerService>();
builder.Services.AddHttpClient<ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Model validation
//builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

// Health checks
builder.Services.AddHealthChecks()
    .AddCheck<OtherServiceHealthCheck>("OtherService", tags: new[] { "other-service" })
    .AddCheck<OtherService2HealthCheck>("OtherService2", tags: new[] { "other-service" })
    .AddCheck<OtherService3HealthCheck>("OtherService3", tags: new[] { "other-service" });

// Add metrics
builder.Services.AddOpenTelemetry()
    .ConfigureResource(config =>
    {
        config.AddService(nameof(MyWebApiDemo));
    })
    //.UseAzureMonitor()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddMeter("MyWebApiDemo.Invoice")
            .AddConsoleExporter()
            .AddPrometheusExporter();
    })
    .WithTracing(tracing =>
    {
        tracing.AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4317");
            });
    });

builder.Services.AddSingleton<InvoiceMetrics>();

// Add Serilog
var logger = new LoggerConfiguration().WriteTo.Seq("http://localhost:5341").CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

app.UseHttpLogging();

// The following route run all registered health checks
app.MapHealthChecks("healthcheck");

// Use the name property to filter health checks
app.MapHealthChecks("/other-service-health-check",
    new HealthCheckOptions() { Predicate = healthCheck => healthCheck.Name == "OtherService" });

// Use the tags to filter health checks
app.MapHealthChecks("/other-services-health-checks",
    new HealthCheckOptions() { Predicate = healthCheck => healthCheck.Tags.Contains("other-service") });

// Use the tags to filter database health checks
app.MapHealthChecks("/database-health-checks",
    new HealthCheckOptions() { Predicate = healthCheck => healthCheck.Tags.Contains("database") });

app.MapPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
    //app.UseExceptionHandler("/error");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
