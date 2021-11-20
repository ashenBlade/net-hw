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
    }
}