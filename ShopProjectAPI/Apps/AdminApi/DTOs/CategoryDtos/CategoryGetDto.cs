using System;

namespace ShopProjectAPI.Apps.AdminApi.DTOs.CategoryDtos
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Image { get; internal set; }
        public int ProductsCount { get; set; }
    }
}
