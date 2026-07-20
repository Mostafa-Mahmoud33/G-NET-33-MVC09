using GymApp.DAl.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GymApp.DAl.DataSeeding
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            ILogger logger, CancellationToken cancellationToken = default)
        {
            try
            {
                // Create Role
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>()
                {
                    new IdentityRole("Admin"),
                    new IdentityRole("SuperAdmin")
                };

                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role.Name!))
                        {
                            var result = await roleManager.CreateAsync(role);
                            if (!result.Succeeded)
                                logger.LogError("Failed To Create Role {role}:{Errors}", role.Name, string.Join(";",
                                    result.Errors.Select(x => x.Description)));
                        }
                    }
                }
                if (!userManager.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        FirstName = "Omar",
                        LastName = "Mohamed",
                        Email = "Omar@root.com",
                        UserName = "Omar",
                        PhoneNumber = "1234567890",
                    };

                    await userManager.CreateAsync(user1, "P@ssw0rd");
                    await userManager.AddToRoleAsync(user1, "Admin");

                    var user2 = new ApplicationUser
                    {
                        FirstName = "Osama",
                        LastName = "Mohamed",
                        Email = "Osama@root.com",
                        UserName = "Osama",
                        PhoneNumber = "1234567866",
                    };

                    await userManager.CreateAsync(user2, "P@ssw0rd");
                    await userManager.AddToRoleAsync(user2, "Admin");
                }
            }
            catch (Exception ex)
            {
                // Log
                logger.LogError(ex, "Seeding Failed");
                throw;
            }
        }
    }
}
