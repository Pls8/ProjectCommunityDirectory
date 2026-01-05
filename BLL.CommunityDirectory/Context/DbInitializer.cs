using DAL.CommunityDirectory.Models.Event;
using DAL.CommunityDirectory.Models.Resources;
using DAL.CommunityDirectory.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CommunityDirectory.Context
{
    //creation of Roles
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // 1. Ensure Database is created
            context.Database.EnsureCreated();

            // 2. Seed Roles
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Staff"));
            }

            // 3. Seed Admin User
            if (!userManager.Users.Any(u => u.Email == "admin@community.com"))
            {
                var adminUser = new AppUser
                {
                    UserName = "admin@community.com",
                    Email = "admin@community.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // 4. Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<CategoryClass>
                {
                    new CategoryClass { Name = "Healthcare" },
                    new CategoryClass { Name = "Education" },
                    new CategoryClass { Name = "Food Security" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // 5. Seed Events
            if (!context.Events.Any())
            {
                await context.Events.AddAsync(new EventClass
                {
                    Title = "Community Health Fair",
                    Description = "Free checkups for everyone.",
                    EventDate = DateTime.Now.AddDays(7),
                    Location = "Main Square"
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
