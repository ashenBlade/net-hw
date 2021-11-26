using System.Linq.Expressions;
using HW10.Models;

namespace HW10.Infrastructure
{
    public class CachingCalculator : BaseCalculatorDecorator
    {
        private readonly CalculatorDbContext _context;

        public CachingCalculator(ICalculator calculator, CalculatorDbContext context) : base(calculator)
        {
            _context = context;
        }

        private bool TryGetResultFromDbContext(string expression, out decimal value)
        {
            value = 0;
            var result = _context.CalculatorCache.Find(expression)?.Value;
            if (!result.HasValue)
                return false;
            value = result.Value;
            return true;
        }

        private void SaveToDb(string expression, decimal value)
        {
            _context.CalculatorCache.Add(new CalculatorCache {Expression = expression, Value = value});
            _context.SaveChanges();
        }

        private decimal CalculateWithCaching(Expression expression)
        {
            var result = Calculator.Calculate(expression);
            SaveToDb(expression.ToString(), result);
            return result;
        }

        public override decimal Calculate(Expression expression)
        {
            return TryGetResultFromDbContext(expression.ToString(), out var result)
                       ? result
                       : CalculateWithCaching(expression);
        }
    }
}