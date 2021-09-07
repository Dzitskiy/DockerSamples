using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.User.Contracts.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        private const string MessageTemplateForUserId = "Пользователя c таким ID[{0}] не было найдено.";
        private const string MessageTemplateForUserName = "Пользователя c таким именем [{0}] не было найдено.";

        public UserNotFoundException(int userId) : base(string.Format(MessageTemplateForUserId, userId))
        {
        }

        public UserNotFoundException(string name) : base(string.Format(MessageTemplateForUserName, name))
        {
            
        }
    }
}