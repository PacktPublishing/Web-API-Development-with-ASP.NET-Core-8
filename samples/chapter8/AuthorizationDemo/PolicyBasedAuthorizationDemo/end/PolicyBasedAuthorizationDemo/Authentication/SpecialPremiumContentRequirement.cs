using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorizationDemo.Authentication;

public class SpecialPremiumContentRequirement : IAuthorizationRequirement
{
    public string Country { get; }

    public SpecialPremiumContentRequirement(string country)
    {
        Country = country;
    }
}
