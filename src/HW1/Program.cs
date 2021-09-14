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
            if (parseResult != 0)
            {
                return parseResult;
            }

            var result = Calculator.Calculate(operation, val1, val2);

            Console.WriteLine($"{val1}{operation}{val2}={result}");
            return 0;
        }
    }
}