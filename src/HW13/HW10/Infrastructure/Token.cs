namespace HW9
{
    public class Token
    {
        public string Value { get; }
        public TokenType TokenType { get; }

        public Token(string value, TokenType type)
        {
            Value = value;
            TokenType = type;
        }
    }
}