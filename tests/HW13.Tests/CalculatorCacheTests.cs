using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HW10;
using HW10.Infrastructure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HW13.Tests
{

    public class CalculatorCacheTests
    {
        private WebApplicationFactory<Startup> NewFactory
            => new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
                                       builder
                                          .ConfigureServices(services =>
                                                                 services
                                                                    .AddDbContext
                                                                         <CalculatorDbContext>(options =>
                                                                                                   options
                                                                                                      .UseInMemoryDatabase("database"))));


        private static HttpRequestMessage GetHttpRequestMessage(string expression)
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

        private static async Task SendAsync(HttpClient client, string expression)
        {
            var message = GetHttpRequestMessage(expression);
            var response = await client.SendAsync(message);
        }

        [Theory]
        [InlineData("@ + @ * @", 10)]
        [InlineData("(10 * @) / 13 + 123", 50)]
        [InlineData("((@ / 10) / 8 - @)", 100)]
        [InlineData("@ + @ - @ - @ - @", 100)]
        [InlineData("@ + 23 * @ + @ - @ / 12", 100)]
        public async Task Calculate(string pattern, int repeat)
        {
            var generator = new ExpressionGenerator(pattern);
            using var host = NewFactory;
            using var client = host.CreateClient();
            for (var i = 0; i < repeat; i++)
            {
                await SendAsync(client, generator.GenerateExpression());
            }
        }
    }
}