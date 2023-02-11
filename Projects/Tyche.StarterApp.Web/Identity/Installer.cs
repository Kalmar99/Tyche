using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tyche.StarterApp.Identity.Storage;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Identity;

public static class Installer
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var componentConfiguration = configuration
            .AddInMemoryVariable("StorageAccount", $"{nameof(IdentityStorageSettings)}:{nameof(IdentityStorageSettings.ConnectionString)}");

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.SlidingExpiration = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.Domain = "localhost";
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthenticationPolicies.Users, policy => policy.RequireRole(IdentityRole.User.ToString(),IdentityRole.AccountAdmin.ToString()));
            options.AddPolicy(AuthenticationPolicies.AccountAdmin, policy => policy.RequireRole(IdentityRole.AccountAdmin.ToString()));
        });

        return services.AddIdentityModule(componentConfiguration);
    }
}