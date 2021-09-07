using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public sealed class User : MutableEntity<int>
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}