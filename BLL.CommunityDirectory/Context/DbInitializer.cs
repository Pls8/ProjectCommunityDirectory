using Microsoft.EntityFrameworkCore;
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
                    FullName = "System Admin",
                    EmailConfirmed = true,
                    IsActive = true,        
                    CreatedAt = DateTime.Now  
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
                var firstCategory = await context.Categories.FirstOrDefaultAsync();

                var sampleEvent = new EventClass
                {
                    Title = "Community Health Fair",
                    Description = "Free checkups for everyone.",
                    EventDate = DateTime.Now.AddDays(7),
                    Location = "Main Square",
                    Organizer = "Community Health Board",
                    ImagePath = "/imgs/FallBackImg.jpg",
                    CategoryId = firstCategory?.Id ?? 1
                };

                await context.Events.AddAsync(sampleEvent);
                await context.SaveChangesAsync();
            }


            // 1. Ensure Resources exist
            if (!context.Resources.Any())
            {
                // Fetch a Category and a User to link the resource to
                var firstCategory = await context.Categories.FirstOrDefaultAsync();
                var adminUser = await context.Users.FirstOrDefaultAsync();

                if (firstCategory != null && adminUser != null)
                {
                    var sampleResources = new List<ResourceClass>
                {
                    new ResourceClass
                    {
                        Name = "City Central Food Bank",
                        Description = "Provides emergency food assistance to families in need.",
                        ContactEmail = "help@cityfoodbank.org",
                        Phone = "555-0123",
                        ContactInfo = "Open Mon-Fri, 9am - 5pm",
                        Address = "123 Charity Lane",
                        City = "Salalah",
                        Website = "https://cityfoodbank.org",
                        IsApproved = true,
                        CategoryId = firstCategory.Id,
                        SubmittedById = adminUser.Id,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    },
                    new ResourceClass
                    {
                        Name = "Community Health Clinic",
                        Description = "Low-cost medical services and vaccinations.",
                        ContactEmail = "info@communityhealth.org",
                        Phone = "555-4567",
                        Address = "456 Wellness Blvd",
                        City = "Salalah",
                        IsApproved = true,
                        CategoryId = firstCategory.Id,
                        SubmittedById = adminUser.Id,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    }
                };

                    await context.Resources.AddRangeAsync(sampleResources);
                    await context.SaveChangesAsync();
                }
            }



        }
    }
}
