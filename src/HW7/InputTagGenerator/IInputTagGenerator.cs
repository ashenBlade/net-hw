using System;
using Microsoft.AspNetCore.Html;

namespace HW7.Infrastructure
{
    public interface IInputTagGenerator
    {
        /// <summary>
        ///     Generates fancy input tag according to meta-information of property
        /// </summary>
        /// <returns>Specialized input tag</returns>
        IHtmlContent GenerateInputTag(Type type);

        IHtmlContent GenerateTextInput();

        IHtmlContent GenerateNumberInput();

        IHtmlContent GenerateSelectInput();
    }
}