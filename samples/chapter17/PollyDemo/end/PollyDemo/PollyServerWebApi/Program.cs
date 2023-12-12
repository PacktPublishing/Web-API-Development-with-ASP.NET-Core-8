using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Use ASP.NET Core rate limiting middleware
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FiveRequestsInThreeSeconds", limiterOptions =>
    {
        limiterOptions.PermitLimit = 5;
        limiterOptions.Window = TimeSpan.FromSeconds(3);
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, _) =>
    {
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later.", CancellationToken.None);
    };
});

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
app.UseRateLimiter();

app.MapGet("/api/slow-response", async () =>
{
    var random = new Random();
    var delay = random.Next(1, 20);
    await Task.Delay(delay * 1000);
    return Results.Ok($"Response delayed by {delay} seconds");
});

app.MapGet("/api/normal-response", async () =>
{
    var random = new Random();
    var delay = random.Next(1, 1000);
    await Task.Delay(delay);
    return Results.Ok($"Response delayed by {delay} milliseconds");
});

app.MapGet("/api/random-failure-response", () =>
{
    var random = new Random();
    var delay = random.Next(1, 100);
    return Task.FromResult(delay > 20 ? Results.Ok($"Response is successful.") : Results.StatusCode(StatusCodes.Status500InternalServerError));
});
app.Run();
