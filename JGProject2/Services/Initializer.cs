using JGProject2.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Used to seed identity the identity user and role for login 
    /// also used to seed to default non identity role for the appliction
    /// </summary>
    public class Initializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IRoleRepository _roleRepository;

        public Initializer(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRoleRepository roleRepository)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }

        public async Task SeedUsersAsync()
        {
            _db.Database.EnsureCreated();

            
            if (!_db.Roles.Any(r => r.Name == "Project Manager"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Project Manager" });
            }

           
            if (!_db.Users.Any(u => u.UserName == "projectmanager@test.com"))
            {
                var user = new ApplicationUser
                {
                    Email = "projectmanager@test.com",
                    UserName = "projectmanager@test.com"
                };

                await _userManager.CreateAsync(user, "Pass1!");
                await _userManager.AddToRoleAsync(user, "Project Manager");
            }
            await _roleRepository.SeedDefaultRole();

        }
    }
}
