namespace MyWebApiDemo.Core.Models;
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset DueDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public OrderStatus Status { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public enum OrderStatus
{
    Draft,
    AwaitPayment,
    Paid,
    Overdue,
    Cancelled
}
