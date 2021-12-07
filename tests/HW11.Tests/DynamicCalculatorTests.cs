using System.Threading.Tasks;
using HW11.Infrastructure;
using HW9;
using Microsoft.Extensions.Logging;
using Xunit;

public class DynamicCalculatorTests
{
    private static DynamicCalculator CreateCalculator()
    {
        return new DynamicCalculator(new ConstantMathExpressionTreeBuilder(new SimpleTokenizer()),
                                     new
                                         CalculatorExceptionHandler(new
                                                                        Logger<
                                                                        CalculatorExceptionHandler>(new LoggerFactory())));
    }

    private static async Task AssertCalculateRightBase(string expression, decimal expected)
    {
        var calculator = CreateCalculator();
        var actual = calculator.Calculate(expression);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Calculate_With4plus4_ShouldCalculateRight()
    {
        await AssertCalculateRightBase("4 + 4", 8);
    }
}