using System.Collections.Generic;

namespace ShopMVC.DTOs
{
    public class CategoryListDto
    {
        public List<CategoryListItemDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
