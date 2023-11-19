using System.ComponentModel.DataAnnotations;

namespace ChatApp.Server.Models;

public class AddOrUpdateUserModel
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; } = string.Empty;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}