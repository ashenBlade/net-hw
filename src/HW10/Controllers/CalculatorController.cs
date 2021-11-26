using HW10.Infrastructure;
using HW9;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HW10.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IMathExpressionTreeBuilder _treeBuilder;
        private readonly ICalculator _calculator;
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(IMathExpressionTreeBuilder treeBuilder,
                                    ICalculator calculator,
                                    ILogger<CalculatorController> logger)
        {
            _treeBuilder = treeBuilder;
            _calculator = calculator;
            _logger = logger;
        }
        // GET
        [Route("Calculator")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Calculate([FromBody] string expression)
        {
            var tree = _treeBuilder.BuildExpression(expression);
            var result = _calculator.Calculate(tree);
            _logger.LogInformation("Evaluating expression: {0}\nResult: {1}", expression, result);
            return new JsonResult(new {Expression = expression, Result = result});
        }
    }
}