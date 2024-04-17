using ConfigurationDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DatabaseOption class as a configuration object
// builder.Services.Configure<DatabaseOption>(builder.Configuration.GetSection(DatabaseOption.SectionName));
// builder.Services.Configure<DatabaseOptions>(DatabaseOptions.SystemDatabaseSectionName, builder.Configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.SystemDatabaseSectionName}"));
// builder.Services.Configure<DatabaseOptions>(DatabaseOptions.BusinessDatabaseSectionName, builder.Configuration.GetSection($"{DatabaseOptions.SectionName}:{DatabaseOptions.BusinessDatabaseSectionName}"));

// Group registration
builder.Services.AddConfig(builder.Configuration);

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
