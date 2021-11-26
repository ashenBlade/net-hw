using HW10.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication
   .CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllersWithViews();
services.AddCalculator();
services.AddDbContext<CalculatorDbContext>(optionsBuilder =>
                                               optionsBuilder.UseSqlite(builder.Configuration
                                                                               .GetConnectionString("SQLiteDevelopmentString")));


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
                       "default",
                       "{controller=Home}/{action=Index}/{id?}");

app.Run();