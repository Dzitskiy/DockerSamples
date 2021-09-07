using System.Collections;
using System.Collections.Generic;
using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class Category : MutableEntity<int>
    {
        public string Name { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
    }
}