using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW9.Pages
{
    public class Calculate : PageModel
    {
        public async Task<int> OnPost(string expression)
        {
            // var visitor = ExpressionVisitor.Visit();
            return 0;
        }
    }
}