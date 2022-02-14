namespace ShopMVC.DTOs
{
    public class CategoryListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryListDtoId { get; set; }
        public CategoryListDto CategoryListDto { get; set; }
    }
}
