using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarWarsForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsForum.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedSuperUser()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<ApplicationUser>(_context);

            var user = new ApplicationUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "FORUMADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                MemberSince = DateTime.Now
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = hasher.HashPassword(user, "admin");
            user.PasswordHash = hashedPassword;

            var hasAdminRole = _context.Roles.Any(role => role.Name == "Admin");

            if (!hasAdminRole)
            {
                await roleStore.CreateAsync(new IdentityRole 
                { 
                    Name = "Admin", 
                    NormalizedName = "ADMIN" 
                });
            }

            var hasSuperUser = _context.Users.Any(u => u.NormalizedUserName == user.NormalizedUserName);

            if(!hasSuperUser)
            {
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "Admin");
            }

            await _context.SaveChangesAsync();
        }
    }
}
