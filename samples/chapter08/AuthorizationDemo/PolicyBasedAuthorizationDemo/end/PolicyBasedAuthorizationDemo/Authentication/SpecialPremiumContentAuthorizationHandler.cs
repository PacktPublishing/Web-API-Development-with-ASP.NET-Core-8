using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PolicyBasedAuthorizationDemo.Authentication;

public class SpecialPremiumContentAuthorizationHandler : AuthorizationHandler<SpecialPremiumContentRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecialPremiumContentRequirement requirement)
    {
        var hasPremiumSubscriptionClaim = context.User.HasClaim(c => c.Type == "Subscription" && c.Value == "Premium");

        if (!hasPremiumSubscriptionClaim)
        {
            return Task.CompletedTask;
        }

        var countryClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Country);
        if (countryClaim == null || string.IsNullOrWhiteSpace(countryClaim.ToString()))
        {
            return Task.CompletedTask;
        }

        if (countryClaim.Value == requirement.Country)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
