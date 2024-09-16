using Microsoft.EntityFrameworkCore;
using SuperCarBookingSystem.Models;
using SuperCarBookingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var mongoDbSetting = builder.Configuration.GetSection("MogoDbSetting").Get<MogoDbSetting>();
builder.Services.Configure<MogoDbSetting>(builder.Configuration.GetSection("MogoDbSetting"));

builder.Services.AddDbContext<CarBookingDbContext>(options => options.UseMongoDB(mongoDbSetting.AtlasURL ?? "", mongoDbSetting.DatabaseName ?? ""));

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
