using DAL.CommunityDirectory.Models.Event;
using DAL.CommunityDirectory.Models.Resources;
using DAL.CommunityDirectory.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CommunityDirectory.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ResourceClass> Resources { get; set; }
        public DbSet<EventClass> Events { get; set; }
        public DbSet<CategoryClass> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional Fluent API configurations (e.g., seeding or specific constraints) can go here
        }
    }
}
