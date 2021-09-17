namespace HW1
{
    // From https://habr.com/ru/post/267231/
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public bool Failure { get; private set; }

        protected Result(bool success, string error)
        {
            Success = success;
            Failure = !success;
            Error = error;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }
    }
}