using AutoMapper;
using ShopProjectAPI.Apps.AdminApi.DTOs.CategoryDtos;
using ShopProjectAPI.Apps.AdminApi.DTOs.ProductDtos;
using ShopProjectAPI.Data.Entity;

namespace ShopProjectAPI.Apps.AdminApi.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryInProductGetDto>();

            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice));
            CreateMap<Product, ProductPostDto>();

        }
    }
}
