using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthorizationDemo.Authentication;

public class LoginModel
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
