using System.Reflection;
using Microsoft.AspNetCore.Html;

namespace HW7.Infrastructure
{
    public interface IInputTagGenerator
    {
        IHtmlContent GenerateInputTagFor(PropertyInfo property);
    }
}