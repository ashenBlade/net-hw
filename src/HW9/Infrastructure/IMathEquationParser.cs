using System.Linq.Expressions;

namespace HW9
{
    public interface IMathEquationParser
    {
        Expression Parse(string equation);
    }
}