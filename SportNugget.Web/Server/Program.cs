using Microsoft.AspNetCore.ResponseCompression;
using SportNugget.Web.Server.Helpers.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region CORS
builder.Services.ConfigureCors();
#endregion

#region IIS Config
builder.Services.ConfigureIISIntegration();
#endregion

#region DB Connection
builder.Services.ConfigureDBContexts(builder.Configuration);
#endregion

#region Identity Server
builder.Services.ConfigureIdentityServer();
#endregion

#region Registrations
builder.Services.ConfigureRegistrations();
#endregion

#region Automapper
builder.Services.ConfigureAutomapper();
#endregion

#region Swagger
builder.Services.ConfigureSwagger();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
