using System.Text;

namespace HW7
{
    public class CamelCaseFormatter : INameFormatter
    {
        public string FormatName(string name)
        {
            var builder = new StringBuilder();
            foreach (var ch in name)
            {
                if (char.IsUpper(ch) || char.IsDigit(ch)) builder.Append(' ');

                builder.Append(ch);
            }

            if (builder.Length > 0 && builder[0] == ' ')
                builder.Remove(0, 1);
            return builder.ToString();
        }
    }
}