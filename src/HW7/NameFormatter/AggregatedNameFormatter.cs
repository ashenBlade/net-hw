namespace HW7.NameFormatter
{
    public class AggregatedNameFormatter : INameFormatter
    {
        private readonly INameFormatter _formatter;
        private readonly AggregatedNameFormatter _inner;

        public AggregatedNameFormatter(INameFormatter formatter, AggregatedNameFormatter inner = null)
        {
            _inner = inner;
            _formatter = formatter;
        }

        public string FormatName(string name)
        {
            name = _formatter.FormatName(name);
            return _inner == null
                       ? name
                       : _inner.FormatName(name);
        }
    }
}