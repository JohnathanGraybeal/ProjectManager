using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to edit a role
    /// </summary>
    public class EditRoleVM
    {
        public int Id { get; set; }
        [Required, MaxLength(30), DisplayName("Role Name")]
        public string Name { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }

        public AppRole GetRoleInstance()
        {
            return new AppRole
            {
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}
