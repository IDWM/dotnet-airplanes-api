using Microsoft.AspNetCore.Identity;

namespace dotnet_airplanes_api.Src.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
