using System.Collections.Generic;

namespace HW9
{
    public class SimpleTokenizer : ITokenizer
    {
        public IEnumerable<Token> Tokenize(string expression)
        {
            expression = RemoveWhitespaces(expression);
            var i = 0;
            while (i < expression.Length)
            {
                Token token;
                var ch = expression[i];
                if (char.IsNumber(ch))
                    token = ReadNumber(expression, i);

                else if (ch == '(')
                    token = new Token("(", TokenType.LeftParenthesis);

                else if (ch == ')')
                    token = new Token(")", TokenType.RightParenthesis);

                else if (IsOperation(ch))
                    token = new Token(ch.ToString(), TokenType.Operation);
                else
                    throw new ParsingException(i, expression);
                i += token.Value.Length;

                yield return token;
            }
        }

        private bool IsOperation(char ch)
        {
            return ch is '+'
                       or '-'
                       or '*'
                       or '/';
        }


        private string RemoveWhitespaces(string expression)
        {
            return expression.Replace(" ", "");
        }

        private Token ReadNumber(string expression, int position)
        {
            var i = position;
            while (i < expression.Length && char.IsDigit(expression, i++)) ;
            return new Token(expression.Substring(position, i), TokenType.Number);
        }
    }
}