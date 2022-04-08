using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Interface for crud operations on projects
    /// </summary>
    public interface IProjectRepository
    {
        Task<Project> ReadAsync(int projectId);

        Task<IEnumerable<Project>> ReadAllAsync();

        Task CreateAsync(Project project );
        Task UpdateAsync(int projectId, Project project);
        Task DeleteAsync(int projectId);
    }
}
