using HW10.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICalculator _calculator;

    public IndexModel(ILogger<IndexModel> logger, ICalculator calculator)
    {
        _logger = logger;
        _calculator = calculator;
    }

    public IActionResult OnGetCalculate([FromQuery] string expression)
    {
        return new JsonResult(new {Result = _calculator.Calculate(expression)});
    }

    public void OnGet() { }
}