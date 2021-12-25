using HW10.Infrastructure;
using HW9;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HW10
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<ITokenizer, SimpleTokenizer>();
            services.AddTransient<IMathExpressionTreeBuilder, ConstantMathExpressionTreeBuilder>();
            services.AddTransient<IMathExpressionSolver, SimpleMathExpressionSolver>();
            services.AddDbContext<CalculatorDbContext>(builder =>
                                                           builder.UseSqlite("SQLiteDevelopmentString"));
            services.AddCalculator();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default",
                                             "{Controller=Calculator}/{Action=Index}/{id?}");
            });
        }
    }
}