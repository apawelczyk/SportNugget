using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportNugget.Web.Client;
using SportNugget.Web.Client.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#region Config File
//builder.Services.AddTransient<WebAssemblyHostConfiguration, WebAssemblyHostConfiguration>();
//builder.Services.AddSingleton<IClientConfiguration, ClientConfiguration>(s => new ClientConfiguration(builder.Configuration));
#endregion

#region Default Registrations
builder.InitializeRegistrations();
#endregion

#region Radzen
builder.InitializeRadzen();
#endregion

#region Mudblazor
builder.InitializeMudBlazor();
#endregion

#region Logging
builder.InitializeLogging();
#endregion

#region HTTP Client
builder.InitializeHTTPClient();
#endregion

#region Authentication/Authorization
builder.InitializeAuthentication();
#endregion

#region State Management
builder.InitializeStateManagement();
#endregion

#region Automapper
builder.ConfigureAutomapper();
#endregion

#region
#endregion

#region
#endregion

await builder.Build().RunAsync();
