using System;
using Xunit;

namespace HW1.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, "+", 2, 3)]
        [InlineData(2, "*", 2, 4)]
        public void Calculate_WithSimpleData_ShouldCalculateAsExpected(int val1, string operation, int val2, int expected)
        {
            // Arrange

            // Act
            var actual = Calculator.Calculate(val1, operation, val2);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}