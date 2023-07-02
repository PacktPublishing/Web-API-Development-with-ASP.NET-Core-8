using ClaimBasedAuthorizationDemo.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ClaimBasedAuthorizationDemo.IntegrationTests;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string AuthenticationScheme = "TestScheme";
    public const string UserNameHeader = "UserName";
    public const string CountryHeader = "Country";
    public const string AccessNumberHeader = "AccessNumber";
    public const string DrivingLicenseNumberHeader = "DrivingLicenseNumber";

    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>();
        if (Context.Request.Headers.TryGetValue(UserNameHeader, out var userName))
        {
            claims.Add(new Claim(ClaimTypes.Name, userName[0]));
        }

        if (Context.Request.Headers.TryGetValue(CountryHeader, out var country))
        {
            claims.Add(new Claim(ClaimTypes.Country, country[0]));
        }

        if (Context.Request.Headers.TryGetValue(AccessNumberHeader, out var accessNumber))
        {
            claims.Add(new Claim(AppClaimTypes.AccessNumber, accessNumber[0]));
        }

        if (Context.Request.Headers.TryGetValue(DrivingLicenseNumberHeader, out var drivingLicenseNumber))
        {
            claims.Add(new Claim(AppClaimTypes.DrivingLicenseNumber, drivingLicenseNumber[0]));
        }

        // You can add more claims here if you want

        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}

// You can customize the options if you want
//public class TestAuthHandlerOptions : AuthenticationSchemeOptions
//{
//    public string UserName { get; set; } = string.Empty;
//}
