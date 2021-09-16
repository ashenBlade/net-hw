using System;
using System.ComponentModel;
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
        public void Calculate_WithInvalidOperationSymbol_ShouldReturn0(int val1, string operand, int val2, int expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Calculate(val1, operand, val2);

            // Assert
            Assert.Equal(expected,  actual);
        }
    }
}