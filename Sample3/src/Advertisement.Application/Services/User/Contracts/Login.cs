namespace Advertisement.Application.Services.User.Contracts
{
    public static class Login
    {
        public sealed class Request
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }

        public sealed class Response
        {
            public string Token { get; set; }
        }
    }
}