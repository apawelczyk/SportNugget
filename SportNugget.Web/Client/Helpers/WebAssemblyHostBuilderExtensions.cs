﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportNugget.Web.Client.Utility.Config;
using Microsoft.Extensions.DependencyInjection;
using SportNugget.Common.API.Interfaces;
using SportNugget.Common.API;
using SportNugget.Common.Configuration.Interfaces;
using SportNugget.Shared.Services.Interfaces;
using SportNugget.Shared.Services;
using SportNugget.Shared.ViewModelBuilders.Interfaces;
using SportNugget.Shared.ViewModelBuilders;
using AutoMapper;
using SportNugget.Shared.MappingConfigurations;
using Radzen;
using Serilog.Core;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Splunk;
using SportNugget.Shared.State.Demos.Interfaces;
using SportNugget.Shared.State.Demos;

namespace SportNugget.Web.Client.Helpers
{
    public static class WebAssemblyHostBuilderExtensions
    {
        public static void InitializeRegistrations(this WebAssemblyHostBuilder builder)
        {
            builder.Services.Scan(o =>
            {
                o.FromCallingAssembly().AddClasses().AsMatchingInterface();
            });
            builder.Services.AddTransient<IDataAccessManager, DataAccessManager>();
            builder.Services.AddTransient<Logging.Interfaces.ILogger, SportNugget.Logging.Logger>();
            builder.Services.AddTransient<ITestService, TestService>();
            builder.Services.AddTransient<ITestViewModelBuilder, TestViewModelBuilder>();
            builder.Services.AddSingleton<ITestState, TestState>();
        }

        public static void InitializeRadzen(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
        }

        public static void InitializeMudBlazor(this WebAssemblyHostBuilder builder)
        {
        //    builder.Services.AddMudServices();
        }

        public static void InitializeLogging(this WebAssemblyHostBuilder builder)
        {
            // TODO: Move to Config File
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            var levelSwitch = new LoggingLevelSwitch();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
                .WriteTo.BrowserHttp(endpointUrl: $"{builder.Configuration[Settings.ServerAPIBaseUrl]}ingest", controlLevelSwitch: levelSwitch) // Sends logs to Web.Server
                .WriteTo.BrowserConsole() // Sends log to Browser console
                .CreateLogger();
        }

        public static void InitializeHTTPClient(this WebAssemblyHostBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddHttpClient(configuration[Settings.ServerAPIClientName], client =>
            {
                client.BaseAddress = new Uri(configuration[Settings.ServerAPIBaseUrl]);
            });
            //.AddHttpMessageHandler(sp =>
            //{
            //    var handler = sp.GetService<AuthorizationMessageHandler>()
            //        .ConfigureHandler(
            //            authorizedUrls: new[] { "https://localhost:44363" },
            //            scopes: new[] { "api-scope" });
            //    return handler;
            //});
            //builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(configuration[Settings.ServerAPIClientName]));
            //builder.Services.AddApiAuthorization();
            builder.Services.AddTransient<IRestClientConfig, RestClientConfig>();
        }

        public static void InitializeAuthentication(this WebAssemblyHostBuilder builder)
        {
            #region
            //builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            //builder.Services.AddOptions();
            //builder.Services.AddAuthorizationCore();
            #endregion

            // Open ID Connect
            //builder.Services.AddOidcAuthentication(opt =>
            //{
            //    opt.ProviderOptions.Authority = "https://localhost:44363";
            //    opt.ProviderOptions.ClientId = "sportnugget_client_id";
            //    opt.ProviderOptions.ResponseType = "code";
            //    opt.ProviderOptions.DefaultScopes.Add("api-scope");
            //    opt.ProviderOptions.PostLogoutRedirectUri = "/";
            //});
        }

        public static void InitializeStateManagement(this WebAssemblyHostBuilder builder)
        {
            //builder.Services.AddSingleton<IAuthState, AuthState>();
        }

        public static void ConfigureOpenIDConnect(IServiceCollection services)
        {

        }

        public static void ConfigureAutomapper(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ViewModelMappingProfile));
        }
    }
}
