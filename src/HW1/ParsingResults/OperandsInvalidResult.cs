namespace HW1
{
    public class OperandsInvalidResult : Result<ParsingErrors>
    {
        private const string ErrorMessage = "Operands are invalid";
        public OperandsInvalidResult() : this(ErrorMessage)
        { }
        public OperandsInvalidResult(string message)
            : base(false, message, ParsingErrors.OperandsInvalid)
        { }
    }
}