using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public interface IInputTagGenerator
    {
        TagBuilder GenerateInputTagFor(PropertyInfo property);
    }
}