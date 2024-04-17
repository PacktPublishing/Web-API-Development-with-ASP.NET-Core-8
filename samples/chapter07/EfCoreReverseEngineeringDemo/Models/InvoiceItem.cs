using System;
using System.Collections.Generic;

namespace EfCoreReverseEngineeringDemo.Models;

public partial class InvoiceItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Quantity { get; set; }

    public decimal Amount { get; set; }

    public Guid InvoiceId { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
