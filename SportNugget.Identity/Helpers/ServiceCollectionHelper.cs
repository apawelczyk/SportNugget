using SportNugget.Identity.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Identity.Services;

namespace SportNugget.Identity.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static void InitializeIdentityServer(this IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential() // Only DEV
                //.AddSigningCredential() // Production Certificate
                .AddOperationalStore(options =>
                {
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30; // Seconds
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddTestUsers(Config.GetUsers().ToList())
                .AddProfileService<ProfileService>();
        }
    }
}
