namespace HW1
{
    public class Result<T> : Result
    {
        public T Value { get; private set; }

        protected internal Result(bool success, string error, T value) : base(success, error)
        {
            Value = value;
        }
    }
}