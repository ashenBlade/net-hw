using System;
using HW9;
using Microsoft.Extensions.Logging;

namespace HW11.Infrastructure
{
    public class CalculatorExceptionHandler : IExceptionHandler,
                                              IExceptionHandler<ParsingException>,
                                              IExceptionHandler<DivideByZeroException>
    {
        private readonly ILogger<CalculatorExceptionHandler> _logger;

        public CalculatorExceptionHandler(ILogger<CalculatorExceptionHandler> logger)
        {
            _logger = logger;
        }

        protected virtual void OnFallBack(Exception exception)
        {
            _logger.LogError("Fallback {0}", exception.Message);
        }

        public void HandleException<T>(T exception) where T : Exception
        {
            if (this is IExceptionHandler<T> handler)
                handler.Handle(exception);
            else
                Handle(( dynamic ) exception);
        }

        public void Handle(Exception ex)
        {
            OnFallBack(ex);
        }

        public void Handle(ParsingException ex)
        {
            _logger.LogError("Parsing exception occured: {0}", ex.Message);
        }

        public void Handle(DivideByZeroException ex)
        {
            _logger.LogError("Division by zero exception occured: {0}", ex.Message);
        }
    }
}