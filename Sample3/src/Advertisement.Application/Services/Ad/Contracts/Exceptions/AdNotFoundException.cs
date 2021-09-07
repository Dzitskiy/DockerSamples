using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Ad.Contracts.Exceptions
{
    public sealed class AdNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Объявление с таким ID[{0}] не было найдено.";
        
        public AdNotFoundException(int adId) : base(string.Format(MessageTemplate, adId))
        {
        }
    }
}