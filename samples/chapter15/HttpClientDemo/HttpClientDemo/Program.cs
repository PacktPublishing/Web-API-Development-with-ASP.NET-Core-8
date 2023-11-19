using HttpClientDemo;

using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("JsonPlaceholder", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    // You can set more options like the default request headers, timeout, etc.
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientDemo");
});

builder.Services.AddHttpClient<UserService>();

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
