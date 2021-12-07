using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication;
using Xunit;
using Xunit.Abstractions;

namespace HW6.Tests
{
    public class AspNetCalculatorTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly FakeWebHostBuilder _builder;
        private HttpClient HttpClient => _builder.CreateClient();

        public AspNetCalculatorTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
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

        [Theory]
        [InlineData("querty")]
        [InlineData("o")]
        [InlineData(".")]
        public async Task ReturnsErrorMessageWith400Code_WhenLeftValueIsInvalid(string left)
        {
            // Arrange
            var expectedErrorMessage = $"\"Could not parse value '{left}' to type System.Int32.\"";
            var expectedCode = HttpStatusCode.BadRequest;

            // Act
            var response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                                                                             $"http://localhost:5001/add?v1={left}&v2=123"));
            var actualMessage = await response.Content.ReadAsStringAsync();
            var actualCode = response.StatusCode;

            // Assert
            Assert.Equal(expectedErrorMessage, actualMessage);
            Assert.Equal(expectedCode, actualCode);
        }

        [Theory]
        [InlineData("qwert")]
        [InlineData("---")]
        [InlineData("O")]
        [InlineData("eleven")]
        public async Task ReturnsErrorMessageWith400Code_WhenRightValueIsInvalid(string right)
        {
            // Arrange
            var expectedErrorMessage = $"\"Could not parse value '{right}' to type System.Int32.\"";
            var expectedCode = HttpStatusCode.BadRequest;

            // Act
            var response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                                                                             $"http://localhost:5001/add?v1=123&v2={right}"));
            var actualMessage = await response.Content.ReadAsStringAsync();
            var actualCode = response.StatusCode;

            // Assert
            Assert.Equal(expectedErrorMessage, actualMessage);
            Assert.Equal(expectedCode, actualCode);
        }


        [Theory]
        [InlineData("summirize")]
        [InlineData("modulo")]
        [InlineData("anigilate")]
        [InlineData("return")]
        [InlineData("")]
        public async Task ReturnsErrorMessageWith400ErrorCode_WhenOperationIsInvalid(string operation)
        {
            // Arrange
            var expectedErrorMessage = $"\"{BinaryOperation.operationNotSupportedErrorMessage(operation)}\"";
            var expectedCode = HttpStatusCode.BadRequest;

            // Act
            var response = await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                                                                             $"http://localhost:5001/{operation}?v1=123&v2=123"));
            var actualMessage = await response.Content.ReadAsStringAsync();
            var actualCode = response.StatusCode;

            // Assert
            Assert.Equal(expectedErrorMessage, actualMessage);
            Assert.Equal(expectedCode, actualCode);
        }
    }
}