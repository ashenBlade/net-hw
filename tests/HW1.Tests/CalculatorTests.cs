using System;
using System.ComponentModel;
using HW2;
using Xunit;

namespace HW1.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(4, "+", 2, 6)]
        [InlineData(4, "-", 2, 2)]
        [InlineData(4, "*", 2, 8)]
        [InlineData(4, "/", 2, 2)]
        public void Calculate_With4FirstOperandAnd2SecondOperand_ShouldCalculateRightForEachOperation(int val1, string operation, int val2, int expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Calculate(val1, operation, val2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "x", 2, 0)]
        [InlineData(123, ":", 0, 0)]
        [InlineData(6746352, "plus", 56789, 0)]
        [InlineData(652, "", 569, 0)]
        [InlineData(65, "^", 59, 0)]
        [InlineData(0b10100, "!", -9889, 0)]
        public void Calculate_WithInvalidOperationSymbol_ShouldReturn0(int val1, string operand, int val2, int expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Calculate(val1, operand, val2);

            // Assert
            Assert.Equal(expected,  actual);
        }

        [Theory]
        [InlineData(1234, "+", 1234, 2468)]
        [InlineData(90, "/", 90, 1)]
        [InlineData(81, "*", 3, 243)]
        [InlineData(-190, "-", 10, -200)]
        [InlineData(9987320, "+", 0, 9987320)]
        [InlineData(1111, "/", 11, 101)]
        [InlineData(-9, "/", 3, -3)]
        [InlineData(5632, "*", 3, 16896)]
        [InlineData(543534, "+", 1282938, 1826472)]
        [InlineData(34, "-", 1282938, -1282904)]
        public void Calculate_WithDifferentValidData_ShouldCalculateRight(int left, string operation, int right, int exprected)
        {
            // Arrange

            // Act
            var actual = Calculator.Calculate(left, operation, right);

            // Assert
            Assert.Equal(exprected, actual);
        }
    }
}