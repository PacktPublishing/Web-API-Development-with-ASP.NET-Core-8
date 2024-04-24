namespace ClaimBasedAuthorizationDemo.Authentication;

public static class AppAuthorizationPolicies
{
    public const string RequireDrivingLicense = "RequireDrivingLicense";
    public const string RequireAccessNumber = "RequireAccessNumber";
    public const string RequireCountry = "RequireCountry";
    public const string RequireDrivingLicenseAndAccessNumber = "RequireDrivingLicenseAndAccessNumber";
}
