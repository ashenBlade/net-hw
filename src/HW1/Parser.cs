using System;
using System.Linq;

namespace HW1
{
    public class Parser
    {
        public const int SuccessCode = 0;
        public const int OperandsInvalidErrorCode = 1;
        public const int OperationNotSupportedErrorCode = 2;

        public static readonly string[] SupportedOperations = { "+", "-", "*", "/" };
        public static Result<ParsingErrors> TryParseArguments(string[] args, out int val1, out string operation, out int val2)
        {
            var isVal1Int = int.TryParse(args[0], out val1);
            operation = args[1];
            var isVal2Int = int.TryParse(args[2], out val2);

            if (!( isVal1Int && isVal2Int ))
            {
                Console.WriteLine($"{args[0]}{args[1]}{args[2]} are not a valid arguments");
                return new OperandsInvalidResult();
            }

            if (!SupportedOperations.Contains(operation))
            {
                Console.WriteLine($"{args[0]}{args[1]}{args[2]} are not a valid arguments "
                                + $"supported operations are {SupportedOperations.Aggregate((c, n) => $"{c} {n}")}");
                return new OperationNotSupportedResult();
            }

            return new SuccessParsingResult();
        }
    }
}