using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class Post
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public Guid? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
