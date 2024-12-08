using dotnet_airplanes_api.Src.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace dotnet_airplanes_api.Src.Data
{
    public static class Seeder
    {
        public static async Task Seed(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            DataContext dataContext,
            IConfiguration config
        )
        {
            await SeedRoles(roleManager);
            await SeedAdminUser(userManager, config);
            await SeedAirplanes(dataContext);
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            var roleNames = new[] { "Admin" };

            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName))
                    continue;

                var role = new Role { Name = roleName };
                var roleResult = await roleManager.CreateAsync(role);

                if (!roleResult.Succeeded)
                    throw new Exception(
                        $"Error creating role '{roleName}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}"
                    );
            }
        }

        private static async Task SeedAdminUser(
            UserManager<User> userManager,
            IConfiguration config
        )
        {
            if (await userManager.Users.AnyAsync(u => u.UserName == config["AdminUser:UserName"]))
                return;

            var userName = config["AdminUser:UserName"]!;
            var email = config["AdminUser:Email"]!;
            var password = config["AdminUser:Password"]!;

            var admin = new User { UserName = userName, Email = email };

            var result = await userManager.CreateAsync(admin, password);
            if (!result.Succeeded)
                throw new Exception(
                    $"Error creating admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );

            var createdUser =
                await userManager.FindByEmailAsync(admin.Email)
                ?? throw new Exception("Failed to retrieve the newly created admin user.");

            var roleAssignmentResult = await userManager.AddToRolesAsync(createdUser, ["Admin"]);

            if (!roleAssignmentResult.Succeeded)
                throw new Exception(
                    $"Error assigning roles to admin user: {string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description))}"
                );
        }

        private static async Task SeedAirplanes(DataContext dataContext)
        {
            if (await dataContext.Airplanes.AnyAsync())
                return;

            var airplanes = new[]
            {
                new Airplane { Model = "Caza", Capacity = 1 },
                new Airplane { Model = "Bombardeo", Capacity = 2 },
                new Airplane { Model = "Transporte", Capacity = 3 },
            };

            await dataContext.Airplanes.AddRangeAsync(airplanes);
            await dataContext.SaveChangesAsync();
        }
    }
}
