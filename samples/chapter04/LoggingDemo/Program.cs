using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
// Clear all the default logging providers.
builder.Logging.ClearProviders();
// Add the Console logging provider.
//builder.Logging.AddConsole();
// Add the Debug logging provider.
//builder.Logging.AddDebug();
// This is to enable the EventLog provider for Windows only. You need to specify the logging level in a `EventLog` section in the appsettings.json file.
//builder.Logging.AddEventLog();

// Configure Serilog
var logger = new LoggerConfiguration()
    .WriteTo.File(formatter: new JsonFormatter(), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
