using System;
using System.Text;

namespace HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            var val1 = int.Parse(args[0]);
            var operation = args[1];
            var val2 = int.Parse(args[2]);
            var result = 0;
            switch (operation)
            {
                case "+":
                    result = val1 + val2;
                    break;
                case "-":
                    result = val1 - val2;
                    break;
                case "*":
                    result = val1 * val2;
                    break;
                case ":":
                case "/":
                    result = val1 / val2;
                    break;
                default:
                    throw new ArgumentException();
            }

            Console.WriteLine($"{val1}{operation}{val2}={result}");
        }
    }
}