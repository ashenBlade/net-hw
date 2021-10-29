using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public static class HtmlExtensions
    {
        public static IHtmlContent FancyEditorForModel<T>(this IHtmlHelper<T> html)
        {
            // 1. Find all properties for editor
            // 2. For each property:
            //      1. Resolve it's type: string, number, enum
            //      2. Find suitable input html tag
            //      3. Find validation attributes
            //      4. Add validation check to html
            //      5. Get appropriate name for property
            //      6. Aggregate all into IHtmlContent (some container)
            // 3. Include all processed properties into main container
            // Extra: add some toppings (prompt, colors...)
            var builder = new HtmlContentBuilder();
            typeof(T).GetProperties()
                     .Select(ConvertPropertyToFancyHtmlEditor)
                     .ToList()
                     .ForEach(content => builder.AppendHtml(content));
            return builder;
        }

        private static IHtmlContent ConvertPropertyToFancyHtmlEditor(PropertyInfo property)
        {
            var builder = new HtmlContentBuilder();

            builder.AppendHtml(GenerateInputTag(property));
            return builder;
        }

        private static IHtmlContent GenerateInputTag(PropertyInfo property)
        {
            var generator = new FancyInputTagGenerator();
            var tag = generator.GenerateInputTag(property.PropertyType);
            return tag;
        }
    }
}