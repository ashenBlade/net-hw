using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HW1
{
    class Program
    {
        static int Main(string[] args)
        {
            var parseResult = Parser.TryParseArguments(args, out var val1, out var operation, out var val2);
            if (parseResult.Value != ParsingErrors.None)
            {
                return (int)parseResult.Value;
            }

            var result = Calculator.Calculate(val1, operation, val2);

            Console.WriteLine($"{val1}{operation}{val2}={result}");
            return (int) ParsingErrors.None;
        }
    }
}