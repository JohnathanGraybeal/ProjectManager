using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Services
{
    /// <summary>
    /// Interface to  read users  create users and assign identity roles 
    /// </summary>
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> ReadAsync(string userName);
        Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);
        Task AssignRoleAsync(string user, string role);
    }
}
