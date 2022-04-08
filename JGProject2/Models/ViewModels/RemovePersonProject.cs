using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to remove a peron from a project
    /// </summary>
    public class RemovePersonProject
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PersonId { get; set; }
        public int ProjectId { get; set; }
       
    }
}
