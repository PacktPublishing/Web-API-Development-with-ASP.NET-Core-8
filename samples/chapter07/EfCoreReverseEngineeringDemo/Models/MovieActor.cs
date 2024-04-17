using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class MovieActor
{
    public Guid MovieId { get; set; }

    public Guid ActorId { get; set; }

    public DateTime UpdateTime { get; set; }

    public virtual Actor Actor { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
