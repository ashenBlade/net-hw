using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using WebApplication;

namespace HW12;

public class FSharpWebApplicationFactory : WebApplicationFactory<StartUp.StartUp>
{
    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder.UseStartup<StartUp.StartUp>());
    }
}