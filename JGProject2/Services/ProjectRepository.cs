using JGProject2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// CRUD operations on projects
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Project project)
        {
            if(project != null)
            {
               
                _db.Projects.Add(project);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int projectId)
        {
            var projectToDelete = await ReadAsync(projectId);
            _db.Projects.Remove(projectToDelete);
            await _db.SaveChangesAsync();
        }

        public async Task<Project> ReadAsync(int projectId)
        {
            var project =  await _db.Projects.FirstOrDefaultAsync(proj => proj.Id == projectId);
            return project;
        }

        public async Task<IEnumerable<Project>> ReadAllAsync()
        {
            return await _db.Projects.ToListAsync();
        }

        public async Task UpdateAsync(int projectId, Project project)
        {
            var projectToUpdate = await ReadAsync(projectId);
            if (project != null)
            {
                projectToUpdate.Name = project.Name;
                projectToUpdate.StartDate = project.StartDate;
                projectToUpdate.DueDate = project.DueDate;
               
            }

            await _db.SaveChangesAsync();
            
        }
    }
}
