using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public static class ExpressionExtensions
    {
        public static async Task<decimal> Evaluate(this Expression expression)
        {
            if (expression is ConstantExpression constantExpression)
                return await Task.FromResult(( decimal ) constantExpression.Value);

            if (expression is BinaryExpression node)
            {
                var left = node.Left.Evaluate();
                var right = node.Right.Evaluate();
                Task.WaitAll(left, right);
                var leftValue = left.Result;
                var rightValue = right.Result;
                return node.NodeType switch
                       {
                           ExpressionType.Add      => leftValue + rightValue,
                           ExpressionType.Subtract => leftValue - rightValue,
                           ExpressionType.Multiply => leftValue * rightValue,
                           ExpressionType.Divide   => leftValue / rightValue,
                           _ => throw new
                                    InvalidOperationException($"Operation: {node.Method} not supported")
                       };
            }

            return 0;
        }
    }
}