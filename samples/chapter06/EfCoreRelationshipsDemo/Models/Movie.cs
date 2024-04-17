using System.Text.Json.Serialization;

namespace EfCoreRelationshipsDemo.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public List<Actor> Actors { get; set; } = new();
    [JsonIgnore]
    public List<MovieActor> MovieActors { get; set; } = new();
}
