using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PolicyBasedAuthorizationDemo.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PolicyBasedAuthorizationDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
    : ControllerBase
{
    // Create an action to register a new user
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AddOrUpdateAppUserModel model)
    {
        // Check if the model is valid
        if (ModelState.IsValid)
        {
            var existedUser = await userManager.FindByNameAsync(model.UserName);
            if (existedUser != null)
            {
                ModelState.AddModelError("", "User name is already taken");
                return BadRequest(ModelState);
            }
            // Create a new user object
            var user = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            // Try to save the user
            var result = await userManager.CreateAsync(user, model.Password);
            // If the user is successfully created, return Created
            if (result.Succeeded)
            {
                return Created();
            }
            // If there are any errors, add them to the ModelState object
            // and return the error to the client
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        // If we got this far, something failed, redisplay form
        return BadRequest(ModelState);
    }

    // Create a Login action to validate the user credentials and generate the JWT token
    [HttpPost("login-new-zealand")]
    public async Task<IActionResult> LoginNewZealand([FromBody] LoginModel model)
    {
        // Get the secret in the configuration

        // Check if the model is valid
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var token = GenerateToken(model.UserName, "New Zealand");
                    return Ok(new { token });
                }
            }
            // If the user is not found, display an error message
            ModelState.AddModelError("", "Invalid username or password");
        }
        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Get the secret in the configuration

        // Check if the model is valid
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var token = GenerateToken(model.UserName, "Australia");
                    return Ok(new { token });
                }
            }
            // If the user is not found, display an error message
            ModelState.AddModelError("", "Invalid username or password");
        }
        return BadRequest(ModelState);
    }

    private string? GenerateToken(string userName, string country)
    {
        var secret = configuration["JwtConfig:Secret"];
        var issuer = configuration["JwtConfig:ValidIssuer"];
        var audience = configuration["JwtConfig:ValidAudiences"];
        if (secret is null || issuer is null || audience is null)
        {
            throw new ApplicationException("Jwt is not set in the configuration");
        }
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(AppClaimTypes.Subscription, "Premium"),
                new Claim(ClaimTypes.Country, country)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        //var jwtToken = new JwtSecurityToken(
        //    issuer: issuer,
        //    audience: audience,
        //    claims: new[]{
        //        new Claim(ClaimTypes.Name, userName)
        //    },
        //    expires: DateTime.UtcNow.AddDays(1),
        //    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
        //);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}
