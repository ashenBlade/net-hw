namespace HW1
{
    public class SuccessParsingResult : Result<ParsingErrors>
    {
        public SuccessParsingResult()
            : base(true, string.Empty, ParsingErrors.None)
        { }
    }
}