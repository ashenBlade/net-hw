using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HW9.Tests
{
    public class CalculatorIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        private HttpClient GetClient()
        {
            return _factory.CreateClient();
        }

        public CalculatorIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private async Task AssertCalculatedRight(string expressionRaw, decimal expected)
        {
            var client = GetClient();
            var url = "/Calculate?expression=" + UrlEncoder.Default.Encode(expressionRaw);
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            if (!decimal.TryParse(responseString, out var actual))
            {
                Assert.True(false,
                            $"Response was not equal to expected\nResponse: {responseString}\nExpected: {expected}\nExpression: {expressionRaw}");
                return;
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Calculate_With4plus4_ShouldReturn8()
        {
            await AssertCalculatedRight("4 + 4", 8);
        }

        [Fact]
        public async Task Calculate_With4plusbracket4multiply2bracket_ShouldReturn12()
        {
            await AssertCalculatedRight("4 + (4 * 2)", 12);
        }

        [Theory]
        [InlineData("1 + 2 + 3 + 4 + 5", "15")]
        [InlineData("1 + 2 + 3 - 4 + 5", "7")]
        [InlineData("1 + 2 + 3 - 4 - 5", "-3")]
        [InlineData("1 * 2 * 3 * 4 * 5", "120")]
        [InlineData("1 * 2 / 3 * 4 * 5", "13.333333333333333333333333334")]
        public async Task Calculate_WithExpressionWithSamePrecedence_ShouldCaclulateRight(
            string expression,
            string expected)
        {
            await AssertCalculatedRight(expression, decimal.Parse(expected));
        }

        [Theory]
        [InlineData("(1 + 2) * 3", "9")]
        [InlineData("(1 * 2) * 3", "6")]
        [InlineData("(1 - 2) * 3", "-3")]
        [InlineData("1 / (2 + 3) + 4", "4.2")]
        public async Task Calculate_WithExpressionWithBrackets_ShouldRespectCalculationOrder(
            string expression,
            string expected)
        {
            await AssertCalculatedRight(expression, decimal.Parse(expected));
        }
    }
}