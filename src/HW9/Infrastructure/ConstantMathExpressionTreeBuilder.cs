using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HW9
{
    public class ConstantMathExpressionTreeBuilder : IMathExpressionTreeBuilder
    {
        private readonly ITokenizer _tokenizer;

        private static readonly Dictionary<string, int> OperatorPrecedence = new()
                                                                             {
                                                                                 {"+", 0}, {"-", 0}, {"*", 1}, {"/", 1}
                                                                             };

        public ConstantMathExpressionTreeBuilder(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        /// <summary>
        ///     Using "Shunting yard algorithm"
        ///     For more info see https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        /// </summary>
        /// <param name="expression">Mathematical expression</param>
        /// <returns>Expression tree representation of that expression</returns>
        public Expression BuildExpression(string expression)
        {
            var output = new Stack<Expression>();
            var operators = new Stack<Token>();
            var currentPosition = 0;
            foreach (var token in _tokenizer.Tokenize(expression))
            {
                if (token.TokenType == TokenType.Number)
                    output.Push(Expression.Constant(int.Parse(token.Value)));
                else if (token.TokenType == TokenType.Operation)
                {
                    while (operators.TryPop(out var upper))
                        if (upper.TokenType != TokenType.LeftParenthesis
                         && PrecedenceOf(token.Value) <= PrecedenceOf(upper.Value))
                            ApplyOperator(output, upper.Value);
                        else
                        {
                            operators.Push(upper);
                            break;
                        }

                    operators.Push(token);
                }
                else if (token.TokenType == TokenType.LeftParenthesis)
                    operators.Push(token);
                else if (token.TokenType == TokenType.RightParenthesis)
                {
                    if (operators.Count == 0)
                        throw new ParsingException(currentPosition, expression)
                              {
                                  Information = "Mismatched parenthesis"
                              };
                    while (operators.TryPop(out var upper) && upper.TokenType != TokenType.LeftParenthesis)
                        ApplyOperator(output, upper.Value);
                }

                currentPosition += token.Value.Length;
            }

            while (operators.Count > 0) ApplyOperator(output, operators.Pop().Value);

            return output.Pop();
        }

        private static void ApplyOperator(Stack<Expression> output, string operation)
        {
            var right = output.Pop();
            var left = output.Pop();
            output.Push(CreateBinaryExpression(operation, left, right));
        }

        private static int PrecedenceOf(string op)
        {
            return OperatorPrecedence[op];
        }

        private static Expression CreateBinaryExpression(string operation, Expression left, Expression right)
        {
            return operation switch
                   {
                       "+" => Expression.Add(left, right),
                       "-" => Expression.Subtract(left, right),
                       "*" => Expression.Multiply(left, right),
                       "/" => Expression.Divide(left, right),
                       _   => throw new InvalidOperationException($"Operation '{operation}' is not supported")
                   };
        }
    }
}