using MinimalApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

//var list = new List<Post>()
//{
//    new() { Id = 1, Title = "First Post", Content = "Hello World" },
//    new() { Id = 2, Title = "Second Post", Content = "Hello Again" },
//    new() { Id = 3, Title = "Third Post", Content = "Goodbye World" },
//};

//// Create a simple posts endpoint for CRUD operations
//app.MapGet("/posts",
//    () => list).WithName("GetPosts").WithOpenApi().WithTags("Posts");
//app.MapPost("/posts",
//    (Post post) =>
//    {
//        list.Add(post);
//        return Results.Created($"/posts/{post.Id}", post);
//    }).WithName("CreatePost").WithOpenApi().WithTags("Posts");
//app.MapGet("/posts/{id}", (int id) =>
//{
//    var post = list.FirstOrDefault(p => p.Id == id);
//    return post == null ? Results.NotFound() : Results.Ok(post);
//}).WithName("GetPost").WithOpenApi().WithTags("Posts");
//app.MapPut("/posts/{id}", (int id, Post post) =>
//{
//    var index = list.FindIndex(p => p.Id == id);
//    if (index == -1)
//    {
//        return Results.NotFound();
//    }
//    list[index] = post;
//    return Results.Ok(post);
//}).WithName("UpdatePost").WithOpenApi().WithTags("Posts");
//app.MapDelete("/posts/{id}", (int id) =>
//{
//    var post = list.FirstOrDefault(p => p.Id == id);
//    if (post == null)
//    {
//        return Results.NotFound();
//    }
//    list.Remove(post);
//    return Results.Ok();
//}).WithName("DeletePost").WithOpenApi().WithTags("Posts");


app.MapGet("/posts", async (IPostService postService) =>
{
    var posts = await postService.GetPostsAsync();
    return posts;
}).WithName("GetPosts").WithOpenApi().WithTags("Posts");
app.MapGet("/posts/{id}", async (IPostService postService, int id) =>
{
    var post = await postService.GetPostAsync(id);
    return post == null ? Results.NotFound() : Results.Ok(post);
}).WithName("GetPost").WithOpenApi().WithTags("Posts");
app.MapPost("/posts", async (IPostService postService, Post post) =>
{
    var createdPost = await postService.CreatePostAsync(post);
    return Results.Created($"/posts/{createdPost.Id}", createdPost);
}).WithName("CreatePost").WithOpenApi().WithTags("Posts");
app.MapPut("/posts/{id}", async (IPostService postService, int id, Post post) =>
{
    try
    {
        var updatedPost = await postService.UpdatePostAsync(id, post);
        return Results.Ok(updatedPost);
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
}).WithName("UpdatePost").WithOpenApi().WithTags("Posts");
app.MapDelete("/posts/{id}", async (IPostService postService, int id) =>
{
    try
    {
        await postService.DeletePostAsync(id);
        return Results.NoContent();
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound();
    }
}).WithName("DeletePost").WithOpenApi().WithTags("Posts");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}