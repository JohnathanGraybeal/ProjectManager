using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Interface for crud operations on roles
    /// </summary>
    public interface IRoleRepository
    {
        Task<AppRole> ReadAsync(int roleId);

        Task<IEnumerable<AppRole>> ReadAllAsync();
        Task CreateAsync(AppRole role);
        Task UpdateAsync(int roleId, AppRole role);
        Task DeleteAsync(int roleId);
        Task SeedDefaultRole();
    }
}
