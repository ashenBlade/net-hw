using System.Net.Http;
using System.Threading.Tasks;
using HW10.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace HW10.Tests
{
    public class CachingCalculatorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CachingCalculatorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private static HttpClient CreateClient()
        {
            var host = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
                                                                                   builder.ConfigureServices(services =>
                                                                                                                 services
                                                                                                                    .AddDbContext
                                                                                                                         <
                                                                                                                         CalculatorDbContext>(options =>
                                                                                                                                                  options
                                                                                                                                                     .UseInMemoryDatabase("database"))));
            return host.CreateClient();
        }


        [Fact]
        public async Task CalculateRight()
        {
            var client = CreateClient();
            var response =
                await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/Home/Index"));
            _testOutputHelper.WriteLine($"{response.StatusCode}");
            _testOutputHelper.WriteLine($"{await response.Content.ReadAsStringAsync()}");
        }
    }
}