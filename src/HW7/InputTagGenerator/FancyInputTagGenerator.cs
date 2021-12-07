using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using HW7.NameFormatter;
using HW7.NameGenerator;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW7.Infrastructure
{
    public class FancyInputTagGenerator : IInputTagGenerator
    {
        private readonly INameFormatter _nameFormatter;
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
                select.InnerHtml.AppendHtml(GetSelectOptionTag(value));
            return select;
        }

        private TagBuilder GetSelectOptionTag(object obj)
        {
            var value = _nameFormatter.FormatName(obj?.ToString() ?? string.Empty);
            var builder = new TagBuilder("option") { Attributes = { { "value", value } } };
            builder.InnerHtml
                   .Append(value);
            return builder;
        }

        private static string GetInputTypeFromDataType(PropertyInfo property)
        {
            return IsIntegerBased(property.PropertyType)
                       ? "number"
                       : IsDateBased(property.PropertyType)
                           ? "date"
                           : "text";
        }

        private static string? GetInputTypeFromAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<DataTypeAttribute>()?.DataType.ToHtmlDataType();
        }
        private static string GetInputTagType(PropertyInfo property)
        {
            return GetInputTypeFromAttribute(property)
                ?? GetInputTypeFromDataType(property);
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


        private static void ApplyValidationAttributes(PropertyInfo property, TagBuilder builder)
        {
            GetValidationAttributes(property)
               .ToList()
               .ForEach(attribute => ApplyValidationAttribute(property, attribute, builder));
        }

        private static int MaxValueForLength(int length)
        {
            var max = 0;
            while (length > 0)
            {
                max = max * 10 + 9;
                length--;
            }

            return max;
        }

        private static (string, string) GetMaxLengthInputAttribute(PropertyInfo property,
                                                                   int value)
        {
            return IsIntegerBased(property.PropertyType)
                       ? ( "max", MaxValueForLength(value).ToString() )
                       : ( "maxlength", value.ToString() );
        }

        private static (string, string) GetMinLengthInputAttribute(PropertyInfo property,
                                                                   int value)
        {
            return IsIntegerBased(property.PropertyType)
                       ? ( "min", ( MaxValueForLength(value - 1) + 1 ).ToString() )
                       : ( "minlength", value.ToString() );
        }

        private static IEnumerable<(string, string)> GetRangeInputAttribute(PropertyInfo property,
                                                                            RangeAttribute range)
        {
            return new[] { ( "min", range.Minimum.ToString() ), ( "max", range.Maximum.ToString() ) };
        }

        private static void ApplyValidationAttribute(PropertyInfo property,
                                                     ValidationAttribute attribute,
                                                     TagBuilder builder)
        {
            var constraints = attribute switch
                              {
                                  MaxLengthAttribute maxLength => new[]
                                                                  {
                                                                      GetMaxLengthInputAttribute(property,
                                                                                                 maxLength.Length)
                                                                  },
                                  MinLengthAttribute minLength => new[]
                                                                  {
                                                                      GetMinLengthInputAttribute(property,
                                                                                                 minLength.Length)
                                                                  },
                                  RangeAttribute range => GetRangeInputAttribute(property, range),
                                  RequiredAttribute required => new[]
                                                                {
                                                                    ( "required",
                                                                      string.Empty )
                                                                },
                                  RegularExpressionAttribute regex => new[]
                                                                      {
                                                                          ( "pattern",
                                                                            regex.Pattern )
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
            if (constraints == null)
                return;

            foreach (var (name, value) in constraints)
                builder.Attributes.Add(name, value);
        }

        private static bool IsIntegerBased(Type type)
        {
            return type == typeof(int)
                || type == typeof(double)
                || type == typeof(long)
                || type == typeof(short)
                || type == typeof(decimal)
                || type == typeof(float);
        }

        private static bool IsDateBased(Type type)
        {
            return type == typeof(DateTime);
        }

    }
}