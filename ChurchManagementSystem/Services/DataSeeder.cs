using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DataSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles= roleManager.Roles.ToList();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";
            string adminRoleName = "Admin";

            // Ensure Role Exists
            var roleExists = await roleManager.RoleExistsAsync(adminRoleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }

            // Check if Admin User Exists
            var existingUser = await userManager.FindByEmailAsync(adminEmail);
            if (existingUser == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "System",
                    FullName = "System Administrator",
                    RoleId = roles.Where(x=>x.Name=="Admin").Select(x=>x.Id).FirstOrDefault()
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
            }
        }
    }
}
