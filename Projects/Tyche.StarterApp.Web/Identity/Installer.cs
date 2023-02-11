using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

public static class Installer
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var componentConfiguration = configuration
            .AddInMemoryVariable("SaltStorageAccount", $"{nameof(SaltStorageSettings)}:{nameof(SaltStorageSettings.ConnectionString)}")
            .AddInMemoryVariable("StorageAccount", $"{nameof(IdentityStorageSettings)}:{nameof(IdentityStorageSettings.ConnectionString)}");

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(ConfigureCookieAuthentication);

        return services
            .AddAuthorization(ConfigureAuthorizationPolicies)
            .AddIdentityModule(componentConfiguration);
    }

    private static void ConfigureAuthorizationPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(AuthenticationPolicies.Users, policy => policy.RequireRole(IdentityRole.User.ToString(),IdentityRole.AccountAdmin.ToString()));
        options.AddPolicy(AuthenticationPolicies.AccountAdmin, policy => policy.RequireRole(IdentityRole.AccountAdmin.ToString()));
    }

    private static void ConfigureCookieAuthentication(CookieAuthenticationOptions options)
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Domain = "localhost";
    }
}