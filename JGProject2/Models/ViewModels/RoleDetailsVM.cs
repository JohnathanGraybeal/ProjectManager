using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to display details about a role
    /// </summary>
    public class RoleDetailsVM
    {
        public int Id { get; set; }
        [ DisplayName("Role Name")]
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
