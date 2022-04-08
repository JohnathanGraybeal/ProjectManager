using JGProject2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// CRUD operations on roles
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(AppRole role)
        {
           
            
                await _db.AppRoles.AddAsync(role);
                await _db.SaveChangesAsync();
            
           
        }

        public async Task DeleteAsync(int roleId)
        {
            var roleToDelete = await ReadAsync(roleId);
           
                _db.AppRoles.Remove(roleToDelete);
                await _db.SaveChangesAsync();
            
               


        }

        public async Task<AppRole> ReadAsync(int roleId)
        {
            return await _db.AppRoles.FirstOrDefaultAsync(ar => ar.Id == roleId);
        }

        public async Task<IEnumerable<AppRole>> ReadAllAsync()
        {
            return await _db.AppRoles.ToListAsync();
        }

        public async Task UpdateAsync(int roleId, AppRole role)
        {
            var toUpdate = await ReadAsync(roleId);
            toUpdate.Name = role.Name;
            toUpdate.Description = role.Description;
            await _db.SaveChangesAsync();
        }

        public async Task SeedDefaultRole()
        {
            var currentRoles = await ReadAllAsync();
            var defaultExist = from def in currentRoles
                               where (def.Name.ToLower() == "member")
                               select def;
            if (defaultExist.Any() == false)
            {
                var role = new AppRole
                {
                    Id = 0,
                    Name = "Member",
                    Description = "Default Role"
                };
                await CreateAsync(role);
            }
        }
    }
}
