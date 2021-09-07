namespace Advertisement.Domain.Shared.Exceptions
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string message) : base(message)
        {
        }
    }
}