using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using HW7.NameFormatter;
using HW7.NameGenerator;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public class FancyInputTagGenerator : IInputTagGenerator
    {
        private readonly AggregatedNameFormatter _nameFormatter;
        public FancyInputTagGenerator()
        {
            _nameFormatter =
                new AggregatedNameFormatter(new CamelCaseFormatter(),
                                            new AggregatedNameFormatter(new FancyNameFormatter()));
        }

        private TagBuilder GenerateSelectTag(PropertyInfo propertyInfo)
        {
            var select = new TagBuilder("select");
            foreach (var value in Enum.GetNames(propertyInfo.PropertyType))
                select.InnerHtml.AppendHtml(GetSelectOption(value));
            return select;
        }

        private TagBuilder GetSelectOption(object obj)
        {
            var value = _nameFormatter.FormatName(obj?.ToString() ?? string.Empty);
            var builder = new TagBuilder("option") { Attributes = { { "value", value } } };
            builder.InnerHtml
                   .Append(value);
            return builder;
        }

        private static string GetInputTagType(PropertyInfo property)
        {
            return GetInputTagTypeFromAttribute(property)
                ?? GetInputTagTypeFromDataType(property);
        }

        private static string GetInputTagTypeFromDataType(PropertyInfo property)
        {
            return IsIntegerBased(property.PropertyType)
                       ? "number"
                       : "text";
        }

        private static string? GetInputTagTypeFromAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<DataTypeAttribute>()?.DataType.ToHtmlDataType();
        }

        public TagBuilder GenerateInputTagFor(PropertyInfo property)
        {
            return property.PropertyType.IsEnum
                       ? GenerateSelectTag(property)
                       : GenerateInputTag(property);
        }

        private TagBuilder GenerateInputTag(PropertyInfo property)
        {
            var builder = new TagBuilder("input") { Attributes = { { "type", GetInputTagType(property) } } };
            ApplyValidationAttributes(property, builder);
            return builder;
        }


        private static IEnumerable<ValidationAttribute> GetValidationAttributes(PropertyInfo property)
        {
            return property.GetCustomAttributes<ValidationAttribute>();
        }

        private static TagBuilder GetBaseInputTag(string type)
        {
            return new TagBuilder("input") { Attributes = { { "type", type } } };
        }


        private void ApplyValidationAttributes(PropertyInfo property, TagBuilder builder)
        {
            GetValidationAttributes(property)
               .ToList()
               .ForEach(attribute => ApplyValidationAttribute(attribute, builder));
        }

        private static void ApplyValidationAttribute(ValidationAttribute attribute, TagBuilder builder)
        {
            IEnumerable<(string Name, string Value)> constraints = attribute switch
                                                                   {
                                                                       MaxLengthAttribute maxLength => new[]
                                                                                                       {
                                                                                                           ( "maxlength",
                                                                                                             maxLength
                                                                                                                .Length
                                                                                                                .ToString() )
                                                                                                       },
                                                                       MinLengthAttribute minLength => new[]
                                                                                                       {
                                                                                                           ( "minlength",
                                                                                                             minLength
                                                                                                                .Length
                                                                                                                .ToString() )
                                                                                                       },
                                                                       RangeAttribute range => new[]
                                                                                               {
                                                                                                   ( "maxlength",
                                                                                                     range.Maximum
                                                                                                          .ToString() ),
                                                                                                   ( "minlength",
                                                                                                     range.Minimum
                                                                                                          .ToString() )
                                                                                               },
                                                                       RequiredAttribute required => new[]
                                                                                                     {
                                                                                                         ( "required",
                                                                                                           string
                                                                                                              .Empty )
                                                                                                     },
                                                                       RegularExpressionAttribute regex => new[]
                                                                                                           {
                                                                                                               ( "pattern",
                                                                                                                 regex
                                                                                                                    .Pattern )
                                                                                                           },
                                                                       EmailAddressAttribute email => new[]
                                                                                                      {
                                                                                                          ( "pattern",
                                                                                                            @"[A-Za-z](\w|\d)+@(\w)+\.(\w)+" )
                                                                                                      },
                                                                       PhoneAttribute phone => new[]
                                                                                               {
                                                                                                   ( "pattern",
                                                                                                     @"\d+" )
                                                                                               },
                                                                       _ => null
                                                                   };
            if (constraints == null) return;

            foreach (var (name, value) in constraints) builder.Attributes.Add(name, value);
        }

        public IHtmlContent GenerateNumberInput(PropertyInfo property)
        {
            var tag = GetBaseInputTag("number");
            return tag;
        }

        private static TagBuilder GenerateBaseSelectInput()
        {
            return new TagBuilder("select");
        }

        public IHtmlContent GenerateSelectInput(PropertyInfo property)
        {
            return GenerateBaseSelectInput();
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