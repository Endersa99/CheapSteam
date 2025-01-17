using CheapSteam.UI.Data;
using ChpStmScraper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using CheapSteam.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAntDesign();
if (!string.IsNullOrEmpty(Configuration.ProxyUrl))
{
    builder.Services.AddSingleton<ChpStmScraper.Services.HttpService>(new ChpStmScraper.Services.HttpService(Configuration.ProxyUrl));
}
else
{
    builder.Services.AddSingleton<ChpStmScraper.Services.HttpService>();
}
builder.Services.AddSingleton(new ScraperService());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
