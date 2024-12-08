using dotnet_airplanes_api.Src.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dotnet_airplanes_api.Src.Data
{
    public class DataContext(DbContextOptions options)
        : IdentityDbContext<
            User,
            Role,
            int,
            IdentityUserClaim<int>,
            UserRole,
            IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>
        >(options)
    {
        public required DbSet<Airplane> Airplanes { get; set; }
    }
}
