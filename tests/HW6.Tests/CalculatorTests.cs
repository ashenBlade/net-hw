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
        [InlineData(2, 6, "8")]
        [InlineData(-123, 678, "555")]
        [InlineData(454, 0, "454")]
        [InlineData(0, -1, "-1")]
        [InlineData(0, 0, "0")]
        public async Task Add_WithValidArgumentsIntegers_ShouldReturnValidResult(int left, int right, string expected)
        {
            await BaseCalculator("add", left, right, expected);
        }

        [Theory]
        [InlineData(10, 1, "9")]
        [InlineData(0, -1, "1")]
        [InlineData(789, 9, "780")]
        [InlineData(-10, 9, "-19")]
        [InlineData(567, 100, "467")]
        [InlineData(123, 100, "23")]
        [InlineData(-1, -1, "0")]
        public async Task Sub_WithValidArgumentsIntegers_ShouldReturnValidResult(int left, int right, string expected)
        {
            await BaseCalculator("sub", left, right, expected);
        }

        [Theory]
        [InlineData(10, 1, "10")]
        [InlineData(0, -1, "0")]
        [InlineData(789, 9, "7101")]
        [InlineData(2, 10, "20")]
        [InlineData(5, 321, "1605")]
        [InlineData(123, 100, "12300")]
        [InlineData(-1, -1, "1")]
        public async Task Mul_WithValidArgumentsIntegers_ShouldReturnValidResult(int left, int right, string expected)
        {
            await BaseCalculator("mul", left, right, expected);
        }

        [Theory]
        [InlineData(10, 1, "10")]
        [InlineData(0, 1, "0")]
        [InlineData(9, 9, "1")]
        [InlineData(121, 11, "11")]
        [InlineData(-121, 11, "-11")]
        [InlineData(97406784, 789, "123456")]
        [InlineData(-1, -1, "1")]
        public async Task Div_WithValidArgumentsIntegers_ShouldReturnValidResult(int left, int right, string expected)
        {
            await BaseCalculator("div", left, right, expected);
        }


        [Fact]
        public async Task Div_WithZeroAsRightOperand_ShouldReturnErrorString()
        {
            await BaseCalculator("div", 1, 0, $"\"{BinaryOperation.divideByZeroErrorMessage}\"");
        }
    }
}