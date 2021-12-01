using System.Collections.Generic;

namespace HW9
{
    public interface ITokenizer
    {
        public IEnumerable<Token> Tokenize(string expression);
    }
}