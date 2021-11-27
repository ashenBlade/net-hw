using System;
using System.Linq.Expressions;
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
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate([FromForm] string expression)
        {
            Expression tree;
            decimal result;
            _logger.LogInformation("Evaluating expression: {0}\n", expression);
            try
            {
                tree = _treeBuilder.BuildExpression(expression);
                result = _calculator.Calculate(tree);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while calculating expression {0}", expression);
                return new JsonResult(new {Success = false, e.Message});
            }

            _logger.LogInformation("Successfully evaluated. Result: {0}", result);
            return new JsonResult(new {Expression = expression, Result = result, Success = true});
        }
    }
}