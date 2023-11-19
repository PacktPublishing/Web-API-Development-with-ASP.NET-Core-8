namespace MyBasicWebApiDemo.Models;

public class Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<Post> Posts { get; set; } = new();
}
