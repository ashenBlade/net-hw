using System.Collections.Generic;
using System.Linq.Expressions;

namespace HW10.Infrastructure
{
    public class CachingCalculator : BaseCalculatorDecorator
    {
        private Dictionary<string, decimal> Values { get; } = new();
        public CachingCalculator(ICalculator calculator) : base(calculator) { }

        public override decimal Calculate(Expression expression)
        {
            return Values.TryGetValue(expression.ToString(), out var result)
                       ? result
                       : Values[expression.ToString()] = Calculator.Calculate(expression);
        }
    }
}