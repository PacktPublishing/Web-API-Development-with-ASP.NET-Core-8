namespace CqrsDemo.Core.Models.Dto;
public class CreateOrUpdateInvoiceItemDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
}
