using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public class SimpleMathExpressionSolver : ExpressionVisitor,
                                              IMathExpressionSolver
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = node.Left.Evaluate();
            var right = node.Right.Evaluate();
            Task.WaitAll(left, right);
            var leftValue = left.Result;
            var rightValue = right.Result;
            return node.NodeType switch
                   {
                       ExpressionType.Add      => Expression.Constant(leftValue + rightValue),
                       ExpressionType.Subtract => Expression.Constant(leftValue - rightValue),
                       ExpressionType.Multiply => Expression.Constant(leftValue * rightValue),
                       ExpressionType.Divide   => Expression.Constant(leftValue / rightValue),
                       _ => throw new
                                InvalidOperationException($"Operation: {node.Method} not supported")
                   };
        }


        public async Task<decimal> SolveAsync(Expression expression)
        {
            return Visit(expression) is ConstantExpression constantExpression
                       ? ( decimal ) constantExpression.Value
                       : throw new Exception($"Invalid expression: {expression}");
        }

        public decimal Solve(Expression expression)
        {
            return SolveAsync(expression)
                  .GetAwaiter()
                  .GetResult();
        }
    }
}