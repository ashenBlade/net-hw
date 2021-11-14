using System;

namespace HW9
{
    public class ParsingException : Exception
    {
        public int Position { get; }
        public string Expression { get; }

        public ParsingException(int position, string expression)
        {
            Position = position;
            Expression = expression;
        }
    }
}