using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public sealed class Ad : MutableEntity<int>
    {
        public enum Statuses
        {
            Created,
            Payed,
            Closed
        }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public Statuses Status { get; set; }
        
        public Category Category { get; set; }
    }
}