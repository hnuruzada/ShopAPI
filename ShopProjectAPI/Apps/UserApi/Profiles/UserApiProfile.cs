using AutoMapper;
using ShopProjectAPI.Apps.UserApi.AccountDtos;
using ShopProjectAPI.Data.Entity;

namespace ShopProjectAPI.Apps.UserApi.Profiles
{
    public class UserApiProfile:Profile
    {
        public UserApiProfile()
        {
            CreateMap<AppUser, AccountGetDto>();
        }
    }
}
