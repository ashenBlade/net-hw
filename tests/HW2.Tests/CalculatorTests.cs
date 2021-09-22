using System;
using Xunit;

namespace HW2.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(2, "+", 2, 4)]
        [InlineData(2, "/", 2, 1)]
        [InlineData(2, "-", 2, 0)]
        [InlineData(2, "*", 2, 4)]
        public void Calculate_WithValidOperands_ShouldCalculateRight(int val1, string op, int val2, int expected)
        {
            // Arrange

            // Act
            var actual = HW2.Calculator.Calculate(val1, op, val2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, "7", 3)]
        [InlineData(2, "_", 10)]
        [InlineData(90, "x", 3)]
        [InlineData(3453, "--", -123)]
        [InlineData(9, "", 3)]
        public void Calculate_WithInvalidOperation_ShouldReturn0(int val1, string op, int val2)
        {
            // Arrange

            // Act
            var actual = HW2.Calculator.Calculate(val1, op, val2);
            // Assert

            Assert.Equal(0, actual);
        }
    }
}