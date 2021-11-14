using System;
using System.Linq.Expressions;

namespace HW9
{
    public class ConstantMathExpressionTreeBuilder : IMathExpressionTreeBuilder
    {
        public Expression BuildExpression(string expression)
        {
            expression = Canonicalize(expression);
            throw new NotImplementedException();
        }

        private static string Canonicalize(string str)
        {
            return str.Replace(" ", "");
        }
    }
}