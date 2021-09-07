namespace Advertisement.Application.Services.Ad.Contracts
{
    public static class GetPaged
    {
        public sealed class Request : Paged.Request
        {
        }

        public sealed class Response : Paged.Response<Response.AdResponse>
        {
            public sealed class AdResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Status { get; set; }
                public decimal Price { get; set; }
            }
        }
    }
}