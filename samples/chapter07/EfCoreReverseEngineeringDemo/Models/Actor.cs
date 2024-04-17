using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class Actor
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MovieActor> MovieActors { get; } = new List<MovieActor>();
}
