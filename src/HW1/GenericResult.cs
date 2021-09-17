using System.Net;

namespace HW1
{
    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected Result(bool success, string message, T value) : base(success, message)
        {
            Value = value;
        }

        public new static Result<T> Fail(string message)
        {
            return new Result<T>(false, message, default);
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }
    }
}