namespace ShopProjectAPI.Apps.AdminApi.DTOs.ProductDtos
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
    }
}
