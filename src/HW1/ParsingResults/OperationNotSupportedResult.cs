using System.Linq;

namespace HW1
{
    public class OperationNotSupportedResult : Result<ParsingErrors>
    {

        private static readonly string ErrorMessage =
            "Operation is not supported!\nSupported operations are:\n"
          + Parser.SupportedOperations.Aggregate((s, n) => $"{s} {n}");

        public OperationNotSupportedResult() : this(ErrorMessage)
        { }

        public OperationNotSupportedResult(string message)
            : base(false, message, ParsingErrors.OperationNotSupported)
        { }
    }
}