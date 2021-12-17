using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HW9
{
    public class SimpleMathExpressionSolver : ExpressionVisitor,
                                              IMathExpressionSolver
    {
        private Dictionary<Expression, Expression[]> ExpressionDependencies { get; } = new();
        private Dictionary<Expression, decimal> Values { get; } = new();

        private Dictionary<ExpressionType, int> Precedence { get; } = new()
                                                                      {
                                                                          {ExpressionType.Add, 1},
                                                                          {ExpressionType.Subtract, 1},
                                                                          {ExpressionType.Multiply, 2},
                                                                          {ExpressionType.Divide, 2}
                                                                      };

        private int PrecedenceOf(ExpressionType type)
        {
            return Precedence[type];
        }

        private void SetDependencies(Expression parent, params Expression[] children)
        {
            ExpressionDependencies[parent] = children;
        }

        private Expression[] GetDependencies(Expression expression)
        {
            return ExpressionDependencies.TryGetValue(expression, out var dependencies)
                       ? dependencies
                       : Array.Empty<Expression>();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            SetDependencies(node, node.Left, node.Right);
            Visit(node.Left);
            Visit(node.Right);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            SetDependencies(node);
            return node;
        }

	private static string _str = string.Empty;

        public async Task<decimal> SolveAsync(Expression expression)
        {
		    for (int i = 0; i < 1000000; i++)
		    {
			    _str += "a";
		    }
            Visit(expression); // Build dependency graph
            return await SolveInnerAsync(expression);
        }

        public IActionResult GetGCInfo()
        {
            var info = GC.GetGCMemoryInfo();
            return new OkResult();
        }

        private async Task<decimal> SolveInnerAsync(Expression expression)
        {
            await Task.WhenAll(GetDependencies(expression)
                                  .Select(SolveInnerAsync)); // Solve dependencies
            await SolveExpression(expression);               // Solve expression itself
            return Values[expression];
        }

        private const int WaitTime = 1000;

        private async Task SolveExpression(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                var left = Values[binaryExpression.Left];
                var right = Values[binaryExpression.Right];
                await Task.Delay(WaitTime);
                Values[expression] = binaryExpression.NodeType switch
                                     {
                                         ExpressionType.Add      => left + right,
                                         ExpressionType.Subtract => left - right,
                                         ExpressionType.Multiply => left * right,
                                         ExpressionType.Divide   => left / right,
                                         _                       => Values[expression]
                                     };
            }
            else if (expression is ConstantExpression constantExpression)
                Values[expression] = ( decimal ) constantExpression.Value;
        }

        public decimal Solve(Expression expression)
        {
            return SolveAsync(expression)
                  .GetAwaiter()
                  .GetResult();
        }
    }
}