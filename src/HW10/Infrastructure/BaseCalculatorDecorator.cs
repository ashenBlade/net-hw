using System;
using System.Linq.Expressions;

namespace HW10.Infrastructure
{
    public abstract class BaseCalculatorDecorator : ICalculator
    {
        protected readonly ICalculator Calculator;

        protected BaseCalculatorDecorator(ICalculator calculator)
        {
            Calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        public virtual decimal Calculate(Expression expression)
        {
            return Calculator.Calculate(expression);
        }
    }
}