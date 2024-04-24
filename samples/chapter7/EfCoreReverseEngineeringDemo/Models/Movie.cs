using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class Movie
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ReleaseYear { get; set; }

    public virtual ICollection<MovieActor> MovieActors { get; } = new List<MovieActor>();
}
