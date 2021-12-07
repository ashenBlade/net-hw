using System.Linq.Expressions;

namespace HW9
{
    public interface IMathExpressionTreeBuilder
    {
        public Expression BuildExpression(string expression);
    }
}