using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class Address
{
    public Guid Id { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public Guid ContactId { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
