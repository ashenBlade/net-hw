using Microsoft.AspNetCore.Mvc;

namespace HW10.Controllers
{
    public class CalculatorController : Controller
    {
        // GET
        [Route("Calculator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}