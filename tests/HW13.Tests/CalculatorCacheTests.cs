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
    public class CalculatorCacheTests : IClassFixture<CalculatorCacheFixture>
    {
        private readonly CalculatorCacheFixture _fixture;
        private HttpClient NewClient => _fixture.Factory.CreateClient();

        public CalculatorCacheTests(CalculatorCacheFixture fixture)
        {
            _fixture = fixture;
        }
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
        [InlineData("@ + @ * @")]
        [InlineData("(10 * @) / 13 + 123")]
        [InlineData("((@ / 10) / 8 - @)")]
        [InlineData("@ + @ - @ - @ - @")]
        [InlineData("@ + 23 * @ + @ - @ / 12")]
        [InlineData("@ / 29 - 23 * @ * @ - @ / 12")]
        [InlineData("@ + @ + @ - 12")]
        [InlineData("@ + @ * (@ / @)")]
        [InlineData("@ - @ * @ * 2")]
        public async Task Calculate(string pattern, int repeat = 100000)
        {
            var generator = new ExpressionGenerator(pattern);
            using var client = NewClient;
            for (var i = 0; i < repeat; i++)
            {
                await SendAsync(client, generator.GenerateExpression());
            }
        }
    }
}