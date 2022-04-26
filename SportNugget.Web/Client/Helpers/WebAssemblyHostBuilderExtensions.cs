using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
using Microsoft.AspNetCore.Components.Authorization;
using SportNugget.Web.Client.Security.Authentication;
using SportNugget.Shared.State.Auth.Interfaces;
using SportNugget.Shared.State.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

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
            builder.Services.AddSingleton<IAuthState, AuthState>();
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

            // Sets up default HTTP client
            // Also allows communication with Identity Server
            builder.Services.AddHttpClient(configuration[Settings.ServerAPIClientName], client =>
            {
                client.BaseAddress = new Uri(configuration[Settings.ServerAPIBaseUrl]);
            })
            .AddHttpMessageHandler(sp =>
            {
                var handler = sp.GetService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { "https://localhost:7013" },
                        scopes: new[] { "api-scope" });
                return handler;
            });
            // Allows CORS with Web API
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(configuration[Settings.ServerAPIClientName]));
            builder.Services.AddApiAuthorization();
            // Configuration for DataAccessManager for which client to use. Base URL is setup according to above.
            builder.Services.AddTransient<IRestClientConfig, RestClientConfig>();
        }

        public static void InitializeAuthentication(this WebAssemblyHostBuilder builder)
        {
            #region
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            #endregion

            // Open ID Connect
            builder.Services.AddOidcAuthentication(opt =>
            {
                opt.ProviderOptions.Authority = "https://localhost:7013/";
                opt.ProviderOptions.ClientId = "579a56d4-cb3c-489c-af75-5f0cfeedb660";
                opt.ProviderOptions.ResponseType = "code id_token";
                opt.ProviderOptions.DefaultScopes.Add("openid");
                opt.ProviderOptions.DefaultScopes.Add("profile");
                opt.ProviderOptions.DefaultScopes.Add("web-api-scope");
                opt.ProviderOptions.PostLogoutRedirectUri = "authentication/logout-callback";
                opt.ProviderOptions.RedirectUri = "authentication/login-callback";
                //opt.ProviderOptions.AdditionalProviderParameters.Add("audience", "SportNugget.Web.Client");
            });
        }

        public static void InitializeStateManagement(this WebAssemblyHostBuilder builder)
        {
            //builder.Services.AddSingleton<IAuthState, AuthState>();
        }

        public static void ConfigureAutomapper(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ViewModelMappingProfile));
        }
    }
}
