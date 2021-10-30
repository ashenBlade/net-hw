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
            var builder = new HtmlContentBuilder();
            var br = new TagBuilder("br") { TagRenderMode = TagRenderMode.SelfClosing };
            typeof(T).GetProperties()
                     .Select(prop => ConvertPropertyToFancyHtmlEditor(prop, html.ViewData.Model))
                     .ToList()
                     .ForEach(content => builder.AppendHtml(content)
                                                .AppendHtml(br));
            return builder;
        }

        private static IHtmlContent ConvertPropertyToFancyHtmlEditor<TModel>(PropertyInfo property, TModel model)
        {
            var builder = new TagBuilder("label") { Attributes = { { "for", property.Name } } };
            var convertPropertyToFancyHtmlEditor = builder
                                                  .InnerHtml
                                                  .AppendHtml(GetFancyLabelName(property))
                                                  .AppendHtml(GetInputTagForProperty(property, model));
            return builder;
        }

        private static TagBuilder GetInputTagForProperty<TModel>(PropertyInfo property, TModel model)
        {
            var input = GenerateFancyInputTag(property);
            input.Attributes.Add("id", property.Name);
            AddValueToInput(property, model, input);
            return input;
        }

        private static void AddValueToInput<TModel>(PropertyInfo property, TModel model, TagBuilder input)
        {
            if (model is not null) input.Attributes["value"] = property.GetValue(model)?.ToString() ?? string.Empty;
        }


        private static string GetFancyLabelName(MemberInfo member)
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

        private static TagBuilder GenerateFancyInputTag(PropertyInfo property)
        {
            var generator = new FancyInputTagGenerator();
            var tag = generator.GenerateInputTagFor(property);
            return tag;
        }
    }
}