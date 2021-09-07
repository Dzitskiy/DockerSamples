using System;

namespace Advertisement.Domain.Shared.Exceptions
{
    public abstract class DomainException : ApplicationException
    {
        protected DomainException(string message): base(message) {}
    }
}