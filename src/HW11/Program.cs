using HW10.Infrastructure;
using HW11.Infrastructure;
using HW9;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();

services.AddTransient<ITokenizer, SimpleTokenizer>();
services.AddTransient<IExceptionHandler, CalculatorExceptionHandler>();
services.AddTransient<IMathExpressionTreeBuilder, ConstantMathExpressionTreeBuilder>();
services.AddTransient<ICalculator, DynamicCalculator>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();