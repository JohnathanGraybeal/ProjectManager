using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.Entities
{
    /// <summary>
    /// Used to create default identity user role
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        [NotMapped]
        public ICollection<string> Roles { get; set; }

        public bool HasRole(string role)
        {
            return Roles.Any(r => r == role);
        }
    }
}
