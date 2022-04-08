using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to remove a role from a person
    /// </summary>
    public class RemoveRoleVM
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
    }
}
