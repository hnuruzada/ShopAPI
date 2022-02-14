using Microsoft.AspNetCore.Identity;

namespace ShopProjectAPI.Data.Entity
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
