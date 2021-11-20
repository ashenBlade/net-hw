using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW9.Pages
{
    public class Calculate : PageModel
    {
        private readonly IMathExpressionSolver _solver;
        private readonly IMathExpressionTreeBuilder _builder;

        public Calculate(IMathExpressionSolver solver, IMathExpressionTreeBuilder builder)
        {
            _solver = solver;
            _builder = builder;
        }

        public IActionResult OnGet([FromQuery] string expression)
        {
            var buildExpression = _builder.BuildExpression(expression);
            Console.WriteLine(buildExpression.ToString());
            var result = _solver.Solve(buildExpression);
            Console.WriteLine($"{result}");
            return new ContentResult {Content = result.ToString(), ContentType = "text/plain"};
        }
    }
}