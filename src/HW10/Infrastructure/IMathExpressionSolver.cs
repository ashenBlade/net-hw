using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public interface IMathExpressionSolver
    {
        public Task<decimal> SolveAsync(Expression expression);
        public decimal Solve(Expression expression);
    }
}