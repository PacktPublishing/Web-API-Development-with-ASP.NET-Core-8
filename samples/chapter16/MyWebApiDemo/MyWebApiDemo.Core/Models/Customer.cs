namespace MyWebApiDemo.Core.Models;
public class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
