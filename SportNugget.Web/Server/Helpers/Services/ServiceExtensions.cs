using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using SportNugget.Business.MappingConfigurations;
using SportNugget.Business.Services.Interfaces;
using SportNugget.Business.Services;
using SportNugget.Business.DataAccess.Interfaces;
using SportNugget.Business.DataAccess;
using SportNugget.Business.Builders.Interfaces;
using SportNugget.Business.Builders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportNugget.Common.API.Interfaces;
using SportNugget.Common.API;
using SportNugget.Web.Server.Utility.Interfaces;
using SportNugget.Web.Server.Utility.API;
using Serilog;

namespace SportNugget.Web.Server.Helpers.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });
        }

        public static void ConfigureDBContexts(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SportnuggetContext>(
                options => options.UseSqlServer(config.GetConnectionString($"SportNugget"),
                opt =>
                {
                    opt.CommandTimeout((int)TimeSpan.FromSeconds(30).TotalSeconds);
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).EnableSensitiveDataLogging());

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<SportnuggetContext>();
        }

        public static void ConfigureHTTPClient(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IDataAccessManager, DataAccessManager>(client =>
            {
                client.BaseAddress = new Uri(config["APIBaseUrl"]);
            });
        }

        public static void ConfigureRegistrations(this IServiceCollection services)
        {
            services.Scan(o =>
            {
                o.FromCallingAssembly().AddClasses().AsMatchingInterface();
            });
            services.AddTransient<SportNugget.Logging.Interfaces.ILogger, SportNugget.Logging.Logger>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<ITestModelBuilder, TestModelBuilder>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(APIMappingProfile));
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
        }

        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.Authority = "https://localhost:44363";
            //    o.Audience = "SportNugget.WebAPI";
            //    o.RequireHttpsMetadata = false;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "https://localhost:44363",
            //        ValidAudience = "SportNugget.WebAPI",
            //        //IssuerSigningKey = new SymmetricSecurityKey("sportnugget_client_id")
            //    };
            //});

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("PublicSecure", policy => policy.RequireClaim("client_id", "secret_client_id"));
            //});
        }

        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            // TODO: Move to Config File
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console());
                //.WriteTo.EventCollector("http://localhost:8088/services/collector", "e4829192-bd42-41b9-baa5-f3f111f54d33")); // Sends logs to Splunk);
        }
    }
}
