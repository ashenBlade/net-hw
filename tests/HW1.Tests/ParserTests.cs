using Xunit;
using HW2;

namespace HW1.Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData(1, "+", 2)]
        [InlineData(8, "*", 10)]
        [InlineData(123, "/", 88)]
        [InlineData(90, "-", 23)]
        public void TryParseArguments_WithValidStringExpression_ShouldReturnSuccessCode(int val1, string operation, int val2)
        {
            // Arrange
            var expression = new[] { val1.ToString(), operation, val2.ToString() };
            // Act
            var actualCode = Parser.TryParseArguments(expression, out var i, out var s, out var t);

            // Assert
            Assert.Equal(ParsingErrors.None, actualCode.Value);
        }

        [Theory]
        [InlineData("123.23", "/", "123")]
        [InlineData("123.23", "+", "a")]
        [InlineData("23", "*", "-123.0")]
        public void TryParseArguments_WithInvalidOperand_ShouldReturnInvalidOperandErrorCode(string left, string operation, string right)
        {
            // Arrange
            var expression = new[] { left, operation, right };

            // Act
            var actualCode = Parser.TryParseArguments(expression, out var i1, out var str, out var i2);

            // Assert
            Assert.Equal(ParsingErrors.OperandsInvalid, actualCode.Value);
        }

        [Theory]
        [InlineData("12", "x", "12")]
        [InlineData("12", ":", "12")]
        [InlineData("12", " ", "12")]
        [InlineData("12", "=", "12")]
        public void TryParseArguments_WithInvalidOperation_ShouldReturnOperationNotSupportedErrorCode(string left, string operation, string right)
        {
            // Arrange
            var expression = new[] { left, operation, right };

            // Act
            var actualCode = Parser.TryParseArguments(expression, out var i1, out var str, out var i2);

            // Assert
            Assert.Equal(ParsingErrors.OperationNotSupported, actualCode.Value);
        }

        [Theory]
        [InlineData("12", "+", "12", 12, "+", 12)]
        [InlineData("-12", "+", "12", -12, "+", 12)]
        [InlineData("100", "-", "12", 100, "-", 12)]
        [InlineData("1", "*", "-10000", 1, "*", -10000)]
        [InlineData("452", "/", "95", 452, "/", 95)]
        public void TryParseArguments_WithValidArguments_ShouldParseRight(string left, string operation, string right,
                                                                          int expectedLeft, string expectedOperation, int expectedRight
            )
        {
            // Arrange
            var expression = new[] { left, operation, right };

            // Act
            var actualCode = Parser.TryParseArguments(expression,
                                                out var actualLeft,
                                                out var actualOperation,
                                                out var actualRight);

            // Assert
            Assert.Equal(ParsingErrors.None, actualCode.Value);
            Assert.Equal(expectedLeft, actualLeft);
            Assert.Equal(expectedOperation, actualOperation);
            Assert.Equal(expectedRight, actualRight);
        }
    }
}