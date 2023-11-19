namespace MyWebApiDemo.Models;

//public class User
//{
//    public int Id { get; set; }

//    [Required]
//    [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of FirstName must be between 3 and 50.")]
//    public string FirstName { get; set; } = string.Empty;

//    [Required]
//    [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of LastName must be between 3 and 50.")]
//    public string LastName { get; set; } = string.Empty;

//    [Required]
//    [Range(1, 120, ErrorMessage = "The value of Age must be between 1 and 120.")]
//    public int Age { get; set; }

//    [Required]
//    [EmailAddress]
//    public string Email { get; set; } = string.Empty;

//    [Required]
//    [Phone]
//    public string PhoneNumber { get; set; } = string.Empty;
//}

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
