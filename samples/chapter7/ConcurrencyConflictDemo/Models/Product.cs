using System.ComponentModel.DataAnnotations;

namespace ConcurrencyConflictDemo.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public int Inventory { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}