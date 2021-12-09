using System.Diagnostics;
using HW8.Models;
using HW8.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HW8.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly ICalculatorService _calculator;

        public CalculatorController(ILogger<CalculatorController> logger, ICalculatorService calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int left, string operation, int right)
        {
            return View(new CalculationResult
                        {
                            Left = left,
                            Operation = operation,
                            Right = right,
                            Result = _calculator.Calculate(left, operation, right)
                        });
        }

        [HttpGet]
        public int Add(int left, int right)
        {
            return _calculator.Calculate(left, "+", right);
        }

        [HttpGet]
        public int Sub(int left, int right)
        {
            return _calculator.Calculate(left, "-", right);
        }

        [HttpGet]
        public int Div(int left, int right)
        {
            return _calculator.Calculate(left, "/", right);
        }

        [HttpGet]
        public int Mul(int left, int right)
        {
            return _calculator.Calculate(left, "*", right);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}