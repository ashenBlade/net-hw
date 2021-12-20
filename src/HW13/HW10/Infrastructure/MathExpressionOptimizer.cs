using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HW9
{
    public class MathExpressionOptimizer : IMathExpressionOptimizer
    {
        private static readonly IReadOnlyDictionary<string, int> Precedences =
            new Dictionary<string, int>
            {
                {PlusOp, 1},
                {MinusOp, 1},
                {MultiplyOp, 2},
                {DivideOp, 2},
                {ConstantOp, int.MaxValue}
            };

        private static readonly IReadOnlyDictionary<ExpressionType, string> OperationStringRepresentation =
            new Dictionary<ExpressionType, string>
            {
                {ExpressionType.Add, PlusOp},
                {ExpressionType.Subtract, MinusOp},
                {ExpressionType.Multiply, MultiplyOp},
                {ExpressionType.Divide, DivideOp},
                {ExpressionType.Constant, ConstantOp}
            };

        private const string PlusOp = "+";
        private const string MinusOp = "-";
        private const string MultiplyOp = "*";
        private const string DivideOp = "/";
        private const string ConstantOp = "C";

        private static int PrecedenceOf(string operation)
        {
            return Precedences[operation];
        }

        private static int PrecedenceOf(ExpressionType expressionType)
        {
            return Precedences[OperationStringRepresentation[expressionType]];
        }

        private static BinaryExpression CreateBinaryExpression(ExpressionType type, Expression left, Expression right)
        {
            return type switch
                   {
                       ExpressionType.Add      => Expression.Add(left, right),
                       ExpressionType.Subtract => Expression.Subtract(left, right),
                       ExpressionType.Multiply => Expression.Multiply(left, right),
                       ExpressionType.Divide   => Expression.Divide(left, right),
                       _                       => throw new Exception($"No operation type: {type}")
                   };
        }

        public static Expression BuildBalancedTree(IReadOnlyList<Expression> tree, ExpressionType type)
        {
            if (tree.Count == 0) return Expression.Constant(0);
            if (tree.Count == 1) return tree[0];

            var result = new Stack<Expression>();
            var i = 0;
            while (tree.Count - i > 1)
            {
                result.Push(CreateBinaryExpression(type, tree[i], tree[i + 1]));
                i += 2;
            }

            if (tree.Count - i == 1) result.Push(tree[i]);

            return result.Count > 1
                       ? BuildBalancedTree(result.ToList(), type)
                       : result.Pop();
        }

        private static List<Expression> CollectSamePrecedenceExpressions(Expression node)
        {
            var precedence = PrecedenceOf(node.NodeType);
            var result = new List<Expression>();

            return result;
        }

        private static IEnumerable<Expression> CollectSamePrecedenceExpressions(Expression node, int precedence)
        {
            if (PrecedenceOf(node.NodeType) != precedence) yield break;
        }

        public Task<Expression> OptimizeAsync(Expression expression)
        {
            throw new NotImplementedException();
        }

        public Expression Optimize(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}