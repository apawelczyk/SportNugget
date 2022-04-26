using SportNugget.Identity.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitializeIdentityServer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseIdentityServer();
app.UseRouting();

app.MapGet("/", () => "Hello Test 9!");

app.Run();
