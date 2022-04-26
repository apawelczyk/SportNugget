using SportNugget.Identity.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Identity.Services;
using Microsoft.EntityFrameworkCore;

namespace SportNugget.Identity.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static void InitializeIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            var migrationAssembly = typeof(WebApplication).GetType().Assembly.GetName().Name;

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
            .AddProfileService<ProfileService>()
            .AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = c => c.UseSqlServer(configuration.GetConnectionString($"SportNugget"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore(opt =>
            {
                opt.ConfigureDbContext = o => o.UseSqlServer(configuration.GetConnectionString($"SportNugget"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
            });
        }
    }
}
