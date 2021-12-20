using System;
using System.Text;

namespace HW13.Tests
{

    public class ExpressionGenerator
    {
        private static Random Random { get; } = new();
        public string Pattern { get; }

        public ExpressionGenerator(string pattern)
        {
            Pattern = pattern;
        }

        public string GenerateExpression()
        {
            var result = new StringBuilder();
            foreach (var ch in Pattern)
            {
                if (ch == '@')
                {
                    result.Append(Random.Next());
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }
    }
}