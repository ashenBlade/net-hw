using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW9.Pages
{
    public class Calculate : PageModel
    {
        private readonly IMathEquationParser _parser;

        public Calculate(IMathEquationParser parser)
        {
            _parser = parser;
        }

        public async Task<int> OnPost(string expression)
        {
            expression = RemoveWhitespaces(expression);
            var x = _parser.Parse(expression);
            return 0;
        }

        private static string RemoveWhitespaces(string raw)
        {
            return raw.Replace(" ", string.Empty);
        }
    }
}