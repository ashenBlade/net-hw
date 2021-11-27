using System.Collections.Generic;
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
            using var host = new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
                                       builder.ConfigureServices(services =>
                                                                     services
                                                                        .AddDbContext<CalculatorDbContext>(options =>
                                                                                                               options
                                                                                                                  .UseInMemoryDatabase("database"))));
            return host.CreateClient();
        }

        private static HttpRequestMessage CreateCalculatorPostMessage(string expression)
        {
            return new HttpRequestMessage(HttpMethod.Post, "https://localhost:5001/Calculator/Calculate")
                   {
                       Content = new FormUrlEncodedContent(new[]
                                                           {
                                                               new KeyValuePair<string?, string?>("expression",
                                                                                                  expression)
                                                           })
                   };
        }

        private static async Task BaseAssert(string expression, string expected)
        {
            using var host = new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
                                       builder.ConfigureServices(services =>
                                                                     services
                                                                        .AddDbContext<CalculatorDbContext>(options =>
                                                                                                               options
                                                                                                                  .UseInMemoryDatabase("database"))));
            var client = host.CreateClient();
            var response = await client.SendAsync(CreateCalculatorPostMessage(expression));
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Calculate_With4plus4_ShouldCalculate8()
        {
            await BaseAssert("4 + 4", "8");
        }
    }
}