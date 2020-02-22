using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BlogAppCore.Persistence
{
    public static class SeedIdentity
    {
        public const string ADMIN_ROLE_NAME = "Admin";
        public const string USER_ROLE_NAME = "User";

        public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            var email = config["Admin:Email"];
            var password = config["Admin:Password"];

            if (String.IsNullOrWhiteSpace(email))
                throw new NullReferenceException("Impossible to create Admin User! Please add Admin credentials to App's secrets.");

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
            }

            await EnsureUserRole(userManager, user, USER_ROLE_NAME);
            await EnsureUserRole(userManager, user, ADMIN_ROLE_NAME);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(USER_ROLE_NAME))
            {
                var role = new IdentityRole
                {
                    Name = USER_ROLE_NAME
                };

                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync(ADMIN_ROLE_NAME))
            {
                var role = new IdentityRole
                {
                    Name = ADMIN_ROLE_NAME
                };

                await roleManager.CreateAsync(role);
            }
        }

        private static async Task EnsureUserRole(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
