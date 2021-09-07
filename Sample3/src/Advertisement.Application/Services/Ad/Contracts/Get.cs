using System.Collections.Generic;
using Advertisement.Domain;

namespace Advertisement.Application.Services.Ad.Contracts
{
    public static class Pay
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }
    }
    
    public static class Get
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {
            public sealed class OwnerResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
            
            public string Name { get; set; }
            public string Status { get; set; }
            public decimal Price { get; set; }
            
            public OwnerResponse Owner { get; set; }
            
            public Category Category { get; set; }
        }
    }
}