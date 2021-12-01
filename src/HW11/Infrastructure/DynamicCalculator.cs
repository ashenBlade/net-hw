using System;
using System.Linq.Expressions;
using HW10.Infrastructure;

namespace HW11.Infrastructure
{
    public class DynamicCalculator : ICalculator
    {
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

        public decimal Calculate(Expression expression)
        {
            return ( dynamic? ) Solve(( dynamic ) expression);
        }
    }
}