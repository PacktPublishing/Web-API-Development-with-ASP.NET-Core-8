namespace MyBasicWebApiDemo.Models;

/// <summary>
/// The post model
/// </summary>
public class Post
{
    /// <summary>
    /// The id of the post
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the post
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The content of the post
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The category id of the post
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// The category of the post
    /// </summary>
    public Category? Category { get; set; }
}
