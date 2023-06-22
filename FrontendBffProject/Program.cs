using GoCloudNative.Bff.Authentication.ModuleInitializers;
using GoCloudNative.Bff.Authentication.OpenIdConnect;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSecurityBff(o =>
{
    o.ConfigureOpenIdConnect(builder.Configuration.GetSection("OIDC"));
    o.LoadYarpFromConfig(builder.Configuration.GetSection("ReverseProxy"));
});

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseSecurityBff();


app.MapFallbackToFile("index.html");

app.Run();