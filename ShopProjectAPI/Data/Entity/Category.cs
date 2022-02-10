using System.Collections.Generic;

namespace ShopProjectAPI.Data.Entity
{
    public class Category:BaseEntity
    {
      
        public string Name { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
