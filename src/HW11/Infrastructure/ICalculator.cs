using System.Linq.Expressions;

namespace HW10.Infrastructure
{
    public interface ICalculator
    {
        public decimal Calculate(Expression expression);
    }
}