using System;

namespace Advertisement.Domain.Shared
{
    public abstract class MutableEntity<TId> : Entity<TId>
    {
        public DateTime? UpdatedAt { get; set; }
    }
}