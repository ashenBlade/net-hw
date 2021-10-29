using System.Reflection;
using Microsoft.AspNetCore.Html;

namespace HW7.Infrastructure
{
    public interface IInputTagGenerator
    {
        IHtmlContent GenerateInputTagFor(PropertyInfo type);
        IHtmlContent GenerateTextInput();
        IHtmlContent GenerateNumberInput();
        IHtmlContent GenerateSelectInput();
    }
}