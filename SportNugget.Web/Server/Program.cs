using Serilog;
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

#region Logging
builder.ConfigureLogging();
#endregion

#region Caching
builder.ConfigureCaching();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseSerilogIngestion();
app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
