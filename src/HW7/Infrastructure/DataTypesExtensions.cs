using System.ComponentModel.DataAnnotations;

namespace HW7.Infrastructure
{
    public static class DataTypesExtensions
    {
        public static string ToHtmlDataType(this DataType type)
        {
            return type switch
                   {
                       DataType.Currency     => "number",
                       DataType.Date         => "date",
                       DataType.Password     => "password",
                       DataType.EmailAddress => "email",
                       DataType.Time         => "time",
                       DataType.Upload       => "file",
                       DataType.Url          => "url",
                       DataType.CreditCard   => "card",
                       DataType.PhoneNumber  => "tel",
                       DataType.ImageUrl     => "image",
                       DataType.DateTime     => "datetime",
                       _                     => "text"
                   };
        }
    }
}