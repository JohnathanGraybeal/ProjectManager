using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Interface for project role crud operations
    /// </summary>
    public interface IProjectRoleRepository
    {
        Task<ProjectRole> ReadAsync(int projectRoleId);

        Task<IEnumerable<ProjectRole>> ReadAllAsync();

        Task CreateAsync(ProjectRole projectRole);
        Task UpdateAsync(int projectRoleId, ProjectRole projectRole);
        Task DeleteAsync(int projectRoleId);
    }
}
