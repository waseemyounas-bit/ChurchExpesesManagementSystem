using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var roles = roleManager.Roles.ToList();

            string adminEmail = "admin@newlightmbc.com";
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
                    RoleId = roles.Where(x => x.Name == "Admin").Select(x => x.Id).FirstOrDefault()
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
            }
            var dbContext = scope.ServiceProvider.GetRequiredService<DataAccess.Data.DataContext>();


            if (!dbContext.SystemRouts.Any())
            {
                var systemRoutes = new List<SystemRout>
        {
            new SystemRout { Id = Guid.NewGuid(), Name = "dashboard" },
            new SystemRout { Id = Guid.NewGuid(), Name = "donations" },
            new SystemRout { Id = Guid.NewGuid(), Name = "members" },
            new SystemRout { Id = Guid.NewGuid(), Name = "vendors" },
            new SystemRout { Id = Guid.NewGuid(), Name = "visitors" },
            new SystemRout { Id = Guid.NewGuid(), Name = "groups" },
            new SystemRout { Id = Guid.NewGuid(), Name = "donation-types" },
            new SystemRout { Id = Guid.NewGuid(), Name = "expense-types" },
            new SystemRout { Id = Guid.NewGuid(), Name = "Roles" },
            new SystemRout { Id = Guid.NewGuid(), Name = "banks" },
            new SystemRout { Id = Guid.NewGuid(), Name = "deposits" },
            new SystemRout { Id = Guid.NewGuid(), Name = "expenses" },
            new SystemRout { Id = Guid.NewGuid(), Name = "administrators" }
        };

                dbContext.SystemRouts.AddRange(systemRoutes);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
