using SportNugget.Identity.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Identity.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SportNugget.Identity.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static void InitializeIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            var migrationAssembly = "SportNugget.Identity";

            services.AddIdentityServer()
            .AddDeveloperSigningCredential() // Only DEV
            //.AddSigningCredential() // Production Certificate
            //.AddOperationalStore(options =>
            //{
            //    options.EnableTokenCleanup = true;
            //    options.TokenCleanupInterval = 30; // Seconds
            //})
            //.AddInMemoryIdentityResources(Config.GetIdentityResources())
            //.AddInMemoryApiResources(Config.GetApiResources())
            //.AddInMemoryClients(Config.GetClients())
            //.AddInMemoryApiScopes(Config.GetApiScopes())
            .AddTestUsers(Config.GetUsers().ToList())
            //.AddProfileService<ProfileService>()
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
