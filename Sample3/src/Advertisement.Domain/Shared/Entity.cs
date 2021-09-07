using System;

namespace Advertisement.Domain.Shared
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}