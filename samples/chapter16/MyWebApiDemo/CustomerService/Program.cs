using Azure.Monitor.OpenTelemetry.AspNetCore;

using Microsoft.AspNetCore.HttpLogging;

using MyWebApiDemo.Core.Services;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerService, MyWebApiDemo.Core.Services.CustomerService>();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(config =>
    {
        config.AddService(nameof(CustomerService));
    })
    .UseAzureMonitor()
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation()
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
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

var logger = new LoggerConfiguration().WriteTo.Seq("http://localhost:5341").CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

app.UseHttpLogging();

app.MapPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
