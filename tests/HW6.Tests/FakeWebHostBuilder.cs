using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using WebApplication;

namespace HW6.Tests
{
    public class FakeWebHostBuilder : WebApplicationFactory<StartUp.StartUp>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                       .ConfigureWebHostDefaults(builder => builder.UseStartup<StartUp.StartUp>());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => { });
        }
    }
}