using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public interface IMathExpressionOptimizer
    {
        public Task<Expression> OptimizeAsync(Expression expression);
        public Expression Optimize(Expression expression);
    }
}