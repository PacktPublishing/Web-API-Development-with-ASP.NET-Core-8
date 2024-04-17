namespace InvoiceApp.WebApi.Models;

public class Invoice
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public Guid ContactId { get; set; }

    public string? Description { get; set; }

    public decimal Amount { get; set; }

    public DateTimeOffset InvoiceDate { get; set; }

    public DateTimeOffset DueDate { get; set; }

    public InvoiceStatus Status { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; } = new();
    public Contact Contact { get; set; } = new();
}
