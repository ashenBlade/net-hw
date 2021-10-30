using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using HW7.NameGenerator;
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
                     .ForEach(content => builder.AppendHtml(content)
                                                .AppendLine());
            return builder;
        }

        private static IHtmlContent ConvertPropertyToFancyHtmlEditor(PropertyInfo property)
        {
            var builder = new HtmlContentBuilder();
            var input = GenerateFancyInputTag(property);
            var label = new TagBuilder("label") { Attributes = { { "for", property.Name } } };
            var name = GetFancyMemberName(property);
            label.InnerHtml.Append(name).AppendHtml(input);
            builder.AppendHtml(label);
            return builder;
        }

        private static string GetFancyMemberName(MemberInfo member)
        {
            var displayName = member.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null) return displayName.DisplayName ?? string.Empty;

            var display = member.GetCustomAttribute<DisplayAttribute>();
            if (display != null) return display.Name ?? string.Empty;

            return FormatNameToFancyCamelCase(member.Name);
        }

        private static string FormatNameToFancyCamelCase(string name)
        {
            return new FancyNameFormatter()
               .FormatName(new CamelCaseFormatter()
                              .FormatName(name));
        }

        private static IHtmlContent GenerateFancyInputTag(PropertyInfo property)
        {
            var generator = new FancyInputTagGenerator();
            var tag = generator.GenerateInputTagFor(property);
            return tag;
        }
    }
}