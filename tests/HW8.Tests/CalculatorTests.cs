using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HW8.Tests
{
    public class CalculatorTests
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CalculatorTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private static string GetCalculatorUrl(string action, string left, string right)
        {
            return $"Calculator/{action}?left={left}&right={right}";
        }

        private async Task BaseTest(string action, string left, string right, string expected)
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync(GetCalculatorUrl(action, left, right));
            response.EnsureSuccessStatusCode();

            var actual = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(-123, 3445)]
        [InlineData(231, 6789)]
        [InlineData(43, 542)]
        [InlineData(0, 0)]
        public async Task Add_WithValidArguments_ShouldCalculateRight(int left, int right)
        {
            await BaseTest("Add", left.ToString(), right.ToString(), ( left + right ).ToString());
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(-123, 3445)]
        [InlineData(231, 6789)]
        [InlineData(43, 542)]
        [InlineData(0, 0)]
        public async Task Sub_WithValidArguments_ShouldCalculateRight(int left, int right)
        {
            await BaseTest("Sub", left.ToString(), right.ToString(), ( left - right ).ToString());
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(-123, 3445)]
        [InlineData(231, 6789)]
        [InlineData(43, 542)]
        [InlineData(0, 0)]
        public async Task Div_WithValidArguments_ShouldCalculateRight(int left, int right)
        {
            await BaseTest("Div", left.ToString(), right.ToString(), ( left / right ).ToString());
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(-123, 3445)]
        [InlineData(231, 6789)]
        [InlineData(43, 542)]
        [InlineData(0, 0)]
        public async Task Mul_WithValidArguments_ShouldCalculateRight(int left, int right)
        {
            await BaseTest("Mul", left.ToString(), right.ToString(), ( left * right ).ToString());
        }


    }
}