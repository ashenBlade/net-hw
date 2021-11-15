using System;

namespace HW9
{
    public class ParsingException : Exception
    {
        public int Position { get; }
        public string Expression { get; }
        public string Information { get; init; } = string.Empty;

        public ParsingException(int position, string expression)
        {
            Position = position;
            Expression = expression;
        }
    }
}