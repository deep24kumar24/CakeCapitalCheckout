using CakeCapitalCheckout.Service;
using Microsoft.Extensions.Configuration;
using Newtonsoft;
using Sentry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllersWithViews();

services.AddTransient<IAirwallexService, AirwallexService>();
services.AddTransient<IXanoService, XanoService>();

services.AddDistributedMemoryCache();

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

SentrySdk.Init(options =>
{
    options.Dsn = "https://df6497e1cebf1897e6c028e541f3f082@o4505647491710976.ingest.sentry.io/4505647530967040";
    options.Debug = true;
    options.AutoSessionTracking = true;
    options.IsGlobalModeEnabled = false;
    options.EnableTracing = true;
    options.TracesSampleRate = 1;
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
