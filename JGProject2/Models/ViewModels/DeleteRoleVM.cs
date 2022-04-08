using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View  model used to delete a role
    /// </summary>
    public class DeleteRoleVM
    {
        public int Id { get; set; }
        [DisplayName("Role Name")]
        public string Name { get; set; }
       
        public string Description { get; set; }
    }
}
