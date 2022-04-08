using JGProject2.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Used for creating a identity role and seeding
    /// </summary>
    public class DbApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbApplicationUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleAsync(string userName, string role)
        {
            var checkRole = await _roleManager.RoleExistsAsync(role);
            if (!checkRole)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await ReadAsync(userName);
            if (user != null)
            {
                if (!user.HasRole(role))
                {
                    await _userManager.AddToRoleAsync(user, role);

                }
            }
        }

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user, string password)
        {
           await _userManager.CreateAsync(user, password);
            return user;
        }

        public async Task<ApplicationUser> ReadAsync(string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            user.Roles = await _userManager.GetRolesAsync(user);
            return user;
        }
    }
}
