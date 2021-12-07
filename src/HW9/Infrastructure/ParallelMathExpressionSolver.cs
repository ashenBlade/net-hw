using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public class ParallelMathExpressionSolver : ExpressionVisitor,
                                                IMathExpressionSolver
    {
        public async Task<decimal> SolveAsync(Expression expression)
        {
            return 0;
        }

        public decimal Solve(Expression expression)
        {
            return SolveAsync(expression)
                  .GetAwaiter()
                  .GetResult();
        }
    }
}