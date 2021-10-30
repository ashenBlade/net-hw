#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public class FancyInputTagGenerator : IInputTagGenerator
    {
        private string GetInputTagType(PropertyInfo property)
        {
            return GetInputTagTypeFromAttribute(property)
                ?? GetInputTagTypeFromDataType(property);
        }

        private static string GetInputTagTypeFromDataType(PropertyInfo property)
        {
            return "text";
        }

        private static string? GetInputTagTypeFromAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<DataTypeAttribute>()?.DataType.ToHtmlDataType();
        }

        public IHtmlContent GenerateInputTagFor(PropertyInfo property)
        {
            var builder = new TagBuilder("input") { Attributes = { { "type", GetInputTagType(property) } } };
            ApplyValidationAttributes(property, builder);
            return builder;
        }

        private IEnumerable<ValidationAttribute> GetValidationAttributes(PropertyInfo property)
        {
            return property.GetCustomAttributes<ValidationAttribute>();
        }

        private TagBuilder GetBaseInput(string type)
        {
            return new TagBuilder("input") { Attributes = { { "type", type } } };
        }


        public IHtmlContent GenerateTextInput(PropertyInfo property)
        {
            return GetBaseInput("text");
        }

        private void ApplyValidationAttributes(PropertyInfo property, TagBuilder builder)
        {
            foreach (var attribute in GetValidationAttributes(property)) ApplyValidationAttribute(attribute, builder);
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
                                                                                                           ( "max-length",
                                                                                                             maxLength
                                                                                                                .Length
                                                                                                                .ToString() )
                                                                                                       },
                                                                       MinLengthAttribute minLength => new[]
                                                                                                       {
                                                                                                           ( "min-length",
                                                                                                             minLength
                                                                                                                .Length
                                                                                                                .ToString() )
                                                                                                       },
                                                                       RangeAttribute range => new[]
                                                                                               {
                                                                                                   ( "max-length",
                                                                                                     range.Maximum
                                                                                                          .ToString() ),
                                                                                                   ( "min-length",
                                                                                                     range.Minimum
                                                                                                          .ToString() )
                                                                                               },
                                                                       RequiredAttribute required => new[]
                                                                                                     {
                                                                                                         ( "required",
                                                                                                           string
                                                                                                              .Empty )
                                                                                                     },

                                                                       _ => null
                                                                   };
            if (constraints == null) return;

            foreach (var (name, value) in constraints) builder.Attributes.Add(name, value);
        }

        public IHtmlContent GenerateNumberInput(PropertyInfo property)
        {
            var tag = GetBaseInput("number");
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