using Xunit;
using HW2;

namespace HW2.Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData(new []{"90", "+", "20"}, 90, "+", 20)]
        [InlineData(new []{"0", "-", "11"}, 0, "-", 11)]
        [InlineData(new []{"20", "/", "2"}, 20, "/", 2)]
        [InlineData(new []{"9", "*", "4"}, 9, "*", 4)]
        [InlineData(new []{"123", "+", "4"}, 123, "+", 4)]
        public void TryParseArguments_WithValidArguments_ShouldReturn0(string[] args, int exVal1, string exOp, int exVal2)
        {

            // Arrange
            var expectedSuccessResultCode = HW2.ParsingErrors.;
            // Act
            var actualResultCode =
                HW2.Parser.TryParseArguments(args, out var actualVal1, out var actualOperation, out var actualVal2);
            // Assert
            Assert.Equal(expectedSuccessResultCode, actualResultCode.Value);
            Assert.Equal(exVal1, actualVal1);
            Assert.Equal(exVal2, actualVal2);
            Assert.Equal(exOp, actualOperation);
        }

        [Theory]
        [InlineData(new[]{"q", "*", "90"}, Parser.OperandsInvalid)]
        [InlineData(new[]{"", "-", "-10"}, Parser.OperandsInvalid)]
        [InlineData(new[]{"O", "+", "90"}, Parser.OperandsInvalid)]
        [InlineData(new[]{"123", "/", "-"}, Parser.OperandsInvalid)]
        [InlineData(new[]{"1", "+", "  "}, Parser.OperandsInvalid)]
        public void TryParseArguments_WithInvalidOperands_ShouldReturn1(string[] args, int expectedCode)
        {
            // Arrange

            // Act
            var actualCode = Parser.TryParseArguments(args, out var val1, out var op, out var val2);
            // Assert
            Assert.Equal(expectedCode, actualCode);
        }

        [Theory]
        [InlineData(new[] {"12", "x", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", "^", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", "%", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", "!", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", " ", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", "&&", "90"}, Parser.OperationNotSupported)]
        [InlineData(new[] {"12", "8", "90"}, Parser.OperationNotSupported)]
        public void TryParseArguments_WithNotSupportedOperation_ShouldReturn2(string[] args, int expectedCode)
        {
            // Arrange

            // Act
            var actualCode = Parser.TryParseArguments(args, out var val1, out var op, out var val2);
            // Assert
            Assert.Equal(actualCode, expectedCode);
        }
    }
}