using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using HW10.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HW10.Tests
{
    public class CachingCalculatorTests
    {
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

        private static WebApplicationFactory<Startup> CreateHost()
        {
            return new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
                                       builder.ConfigureServices(services =>
                                                                     services
                                                                        .AddDbContext<CalculatorDbContext>(options =>
                                                                                                               options
                                                                                                                  .UseInMemoryDatabase("database"))));
        }

        private static async Task BaseAssert(string expression, string expectedString)
        {
            using var host = CreateHost();
            using var client = host.CreateClient();
            using var message = CreateCalculatorPostMessage(expression);
            using var response = await client.SendAsync(CreateCalculatorPostMessage(expression));
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


        [Theory]
        [InlineData("(1 + 1) * 4", "8")]
        [InlineData("4 * 2 / (11 - 3)", "1")]
        [InlineData("4 * 30 / (5 - 3)", "60")]
        [InlineData("4 + 2 / (11 - 7)", "4.5")]
        public async Task Calculate_WithExpressionsWithParenthesis_ShouldCalculateRespectingPresedence(
            string expression,
            string expected)
        {
            await BaseAssert(expression, expected);
        }

        [Theory]
        [InlineData("1 + 1 + 1 + 1")]
        [InlineData("1 - 1000 * 90 / 1")]
        [InlineData("1 - 1000")]
        [InlineData("90 / 2")]
        [InlineData("123 / 2 * 34 / (15 + 3)")]
        public async Task Calculate_WithRepetitiveExpressions_ShouldReturnResultFasterSecondTime(string expression)
        {
            using var host = CreateHost();
            using var client = host.CreateClient();
            using var message = CreateCalculatorPostMessage(expression);

            async void Action()
            {
                await client.SendAsync(message);
            }

            var first = await MeasureTime(Action);
            var second = await MeasureTime(Action);
            Assert.True(first > second);
        }

        private static async Task<TimeSpan> MeasureTime(Action action)
        {
            var timer = new Stopwatch();
            timer.Start();
            action();
            timer.Stop();
            return timer.Elapsed;
        }
    }
}