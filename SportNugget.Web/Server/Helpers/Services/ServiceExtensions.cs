﻿using Microsoft.AspNetCore.Builder;
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
using System.Text;
using SportNugget.Caching.Interfaces;
using SportNugget.Caching;
using StackExchange.Redis;

namespace SportNugget.Web.Server.Helpers.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => {
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
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

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<SportnuggetContext>();
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = "https://localhost:7013";
                o.Audience = "SportNugget.Web.API";
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7013",
                    ValidAudience = "SportNugget.Web.API",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ServerSecretKey12345"))
                };
            });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Policy1", policy => policy.RequireClaim("client_id", "sportnugget_web_api_client_id"));
            });
        }

        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            // TODO: Move to Config File
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console());
                //.WriteTo.EventCollector("http://localhost:8088/services/collector", "e4829192-bd42-41b9-baa5-f3f111f54d33")); // Sends logs to Splunk);
        }


        public static void ConfigureCaching(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            var redisOptions = new ConfigurationOptions()
            {
                KeepAlive = 0,
                AllowAdmin = true,
                EndPoints = { { "127.0.0.1", 5002 } },
                ConnectTimeout = 5000,
                ConnectRetry = 5,
                SyncTimeout = 5000,
                AbortOnConnectFail = false,
            };

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "http://localhost:5002"; //configuration[Settings.RedisConnectionString];
                options.InstanceName = "SportNugget-Web_";
                options.ConfigurationOptions = redisOptions;
            });

            builder.Services.AddTransient<ICacheManager, DistributedCacheManager>();
        }
    }
}
