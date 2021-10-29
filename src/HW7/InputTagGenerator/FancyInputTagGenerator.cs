using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public class FancyInputTagGenerator : IInputTagGenerator
    {
        public IHtmlContent GenerateInputTag(Type type)
        {
            if (IsEnum(type)) return GenerateSelectInput();
            if (IsIntegerBased(type)) return GenerateNumberInput();
            return GenerateTextInput();
        }

        private IHtmlContent GetBaseInput(string type)
        {
            var builder = new HtmlContentBuilder();
            var input = new TagBuilder("input");
            input.Attributes.Add("type", type);
            builder.AppendHtml(input);
            return builder;
        }

        public IHtmlContent GenerateTextInput()
        {
            return GetBaseInput("text");
        }

        public IHtmlContent GenerateNumberInput()
        {
            return GetBaseInput("number");
        }

        public IHtmlContent GenerateSelectInput()
        {
            return GetBaseInput("select");
        }

        private static bool IsStringBased(Type type)
        {
            return type == typeof(string);
        }

        private static bool IsIntegerBased(Type type)
        {
            return type == typeof(int)
                || type == typeof(long)
                || type == typeof(short);
        }

        private static bool IsEnum(Type type)
        {
            return type.IsEnum;
        }
    }
}