namespace CqrsDemo.Core.Models.Dto;
public class CreateOrUpdateInvoiceDto
{
    public string ContactName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTimeOffset InvoiceDate { get; set; }

    public DateTimeOffset DueDate { get; set; }

    public InvoiceStatus Status { get; set; }

    public List<CreateOrUpdateInvoiceItemDto> InvoiceItems { get; set; } = new();
}
