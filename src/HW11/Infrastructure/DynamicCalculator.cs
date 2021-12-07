using System;
using System.Linq.Expressions;
using HW10.Infrastructure;
using HW9;

namespace HW11.Infrastructure
{
    public class DynamicCalculator : ICalculator
    {
        private readonly IMathExpressionTreeBuilder _treeBuilder;
        private readonly IExceptionHandler _handler;

        public DynamicCalculator(IMathExpressionTreeBuilder treeBuilder, IExceptionHandler handler)
        {
            _treeBuilder = treeBuilder;
            _handler = handler;
        }

        private decimal Solve(BinaryExpression binary)
        {
            var left = Solve(( dynamic ) binary.Left);
            var right = Solve(( dynamic ) binary.Right);
            return binary.NodeType switch
                   {
                       ExpressionType.Add => left + right,
                       ExpressionType.Subtract => left - right,
                       ExpressionType.Divide => left / right,
                       ExpressionType.Multiply => left * right,
                       _ => throw new InvalidOperationException($"Operation {binary.NodeType} is not supported")
                   };
        }

        private decimal Solve(ConstantExpression constant)
        {
            return ( decimal ) constant.Value;
        }

        public decimal Calculate(string expression)
        {
            try
            {
                return ( dynamic? ) Solve(( dynamic ) _treeBuilder.BuildExpression(expression));
            }
            catch (Exception e)
            {
                _handler.HandleException(e);
                return 0;
            }
        }
    }
}