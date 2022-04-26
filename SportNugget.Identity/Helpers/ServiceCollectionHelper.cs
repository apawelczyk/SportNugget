using SportNugget.Identity.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Identity.Services;

namespace SportNugget.Identity.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static void InitializeIdentityServer(this IServiceCollection services)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
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
