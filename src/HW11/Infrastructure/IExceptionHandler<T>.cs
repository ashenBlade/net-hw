using System;

namespace HW11.Infrastructure
{
    public interface IExceptionHandler<in T> where T : Exception
    {
        void Handle(T ex);
    }
}