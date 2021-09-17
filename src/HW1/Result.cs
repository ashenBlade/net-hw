using System;

namespace HW1
{
    // From https://habr.com/ru/post/267231/
    public class Result
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public bool Failure { get; private set; }

        protected Result(bool success, string message)
        {
            Success = success;
            Failure = !success;
            Message = message;
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
    }
}