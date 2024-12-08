using Microsoft.AspNetCore.Identity;

namespace dotnet_airplanes_api.Src.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
