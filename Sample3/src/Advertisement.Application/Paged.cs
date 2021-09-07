using System.Collections.Generic;

namespace Advertisement.Application
{
    public static class Paged
    {
        public abstract class Request
        {
            public int Offset { get; set; } = 0;
            public int Limit { get; set; } = 10;
        }

        public abstract class Response<T>
        {
            public int Total { get; set; }
            public int Limit { get; set; }
            public int Offset { get; set; }
            
            public IEnumerable<T> Items { get; set; }
        } 
    }
}