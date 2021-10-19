using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication;
using Xunit;

namespace HW6.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<StartUp.StartUp>>
    {
        private readonly FakeWebHostBuilder _builder;
        private HttpClient HttpClient => _builder.CreateClient();

        public UnitTest1()
        {
            _builder = new FakeWebHostBuilder();
        }

        private async Task BaseCalculator(string operation, int left, int right, string expected)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get,
                                             new Uri($"http://localhost:5001/{operation}?v1={left}&v2={right}"));
            var response = await HttpClient.SendAsync(msg);
            var actual = await response.Content.ReadAsStringAsync();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 2, "3")]
        public async Task Add_WithValidArguments_ShouldReturnValidResult(int left, int right, string expected)
        {
            await BaseCalculator("add", left, right, expected);
        }
    }
}