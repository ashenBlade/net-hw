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

        private static async Task BaseAssert(string expression, string expectedString)
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
            var actual = decimal.Parse(await response.Content.ReadAsStringAsync());
            var expected = decimal.Parse(expectedString);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Calculate_With4plus4_ShouldCalculate8()
        {
            await BaseAssert("4 + 4", "8");
        }

        [Theory]
        [InlineData("1 + 2 + 3 + 4", "10")]
        [InlineData("1 * 2", "2")]
        [InlineData("1 * 2 * 3 * 4", "24")]
        [InlineData("100 - 2 + 3 + 4", "105")]
        [InlineData("11 + 21 - 34", "-2")]
        [InlineData("1 * 2 + 3 - 4", "1")]
        [InlineData("1 + 56 - 11 * 2", "35")]
        [InlineData("1 + 56 - 22 / 2", "46")]
        [InlineData("1 + 56 + 22 * 2", "101")]
        public async Task Calculate_WithSimpleExpression_ShouldCalculateRight(string expression, string expected)
        {
            await BaseAssert(expression, expected);
        }
    }
}