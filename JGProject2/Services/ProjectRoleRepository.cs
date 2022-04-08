using JGProject2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// CRUD operations on projects
    /// </summary>
    public class ProjectRoleRepository : IProjectRoleRepository
    {
        private ApplicationDbContext _db;

        public ProjectRoleRepository( ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(ProjectRole projectRole)
        {
            if (projectRole != null)
            {
                await _db.ProjectRoles.AddAsync(projectRole);
                await _db.SaveChangesAsync();

            }
        }

        public async Task DeleteAsync(int projectRoleId)
        {
            var projectRole = await ReadAsync(projectRoleId);


            _db.ProjectRoles.Remove(projectRole);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectRole>> ReadAllAsync()
        {
            return await _db.ProjectRoles.ToListAsync();
        }

        public async Task<ProjectRole> ReadAsync(int projectRoleId)
        {
            return await _db.ProjectRoles.FirstOrDefaultAsync(pr => pr.Id == projectRoleId);
        }

        public async Task UpdateAsync(int projectRoleId, ProjectRole projectRole)
        {
            var toUpdate = await ReadAsync(projectRoleId);
            if (projectRole != null)
            {
                toUpdate.PersonId = projectRole.PersonId;
                toUpdate.Person = projectRole.Person;
                toUpdate.ProjectId = projectRole.ProjectId;
                toUpdate.Project = projectRole.Project;
                toUpdate.RoleId = projectRole.RoleId;
                toUpdate.AppRole = projectRole.AppRole;
                toUpdate.HourlyRate = projectRole.HourlyRate;


                await _db.SaveChangesAsync();
            }
        }
    }
}
