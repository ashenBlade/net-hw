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

        [HttpGet]
        public IActionResult Add(int left, int right)
        {
            return View("Result",
                        new CalculationResult
                        {
                            Left = left,
                            Operation = "+",
                            Right = right,
                            Result = _calculator.Calculate(left, "+", right)
                        });
        }

        [HttpGet]
        public IActionResult Sub(int left, int right)
        {
            return new JsonResult(_calculator.Calculate(left, "-", right));
        }

        [HttpGet]
        public IActionResult Div(int left, int right)
        {
            return new JsonResult(_calculator.Calculate(left, "/", right));
        }

        [HttpGet]
        public IActionResult Mul(int left, int right)
        {
            return new JsonResult(_calculator.Calculate(left, "*", right));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}