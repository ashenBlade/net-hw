namespace HW1
{
    public class SuccessParsingResult : Result<ParsingErrors>
    {
        protected SuccessParsingResult()
            : base(true, string.Empty, ParsingErrors.None)
        { }
    }
}