using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HW9.Tests
{
    public class TokenizerTests
    {
        private readonly SimpleTokenizer _tokenizer;

        public TokenizerTests()
        {
            _tokenizer = new SimpleTokenizer();
        }

        private void AssertTokensEquals(IEnumerable<Token> exp, IEnumerable<Token> act)
        {
            var expected = exp.ToList();
            var actual = act.ToList();
            Assert.Equal(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Value, actual[i].Value);
                Assert.Equal(expected[i].TokenType, actual[i].TokenType);
            }
        }

        [Fact]
        public void Tokenize_With4plus4_ShouldReturn3TokensValid()
        {
            // Arrange
            var expression = "4 + 4";
            var expected = new[]
                           {
                               new Token("4", TokenType.Number), new Token("+", TokenType.Operation),
                               new Token("4", TokenType.Number)
                           };
            // Act
            var actual = _tokenizer.Tokenize(expression);
            // Assert
            AssertTokensEquals(expected, actual);
        }

        [Fact]
        public void Tokenize_With4plus4InParenthesis_ShouldReturn5TokensValid()
        {
            // Arrange
            var expression = "(4 + 4)";
            var expected = new[]
                           {
                               new Token("(", TokenType.LeftParenthesis), new Token("4", TokenType.Number),
                               new Token("+", TokenType.Operation), new Token("4", TokenType.Number),
                               new Token(")", TokenType.RightParenthesis)
                           };
            // Act
            var actual = _tokenizer.Tokenize(expression);
            // Assert
            AssertTokensEquals(expected, actual);
        }
    }
}