using System;

namespace HW11.Infrastructure
{
    public interface IExceptionHandler
    {
        void HandleException<T>(T exception) where T : Exception;
    }
}