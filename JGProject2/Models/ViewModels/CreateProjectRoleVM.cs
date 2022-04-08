using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// Used to create an entry in the intermediate table
    /// </summary>
    public class CreateProjectRoleVM
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int RoleId { get; set; }
        public AppRole AppRole { get; set; }
        [Range(8.00, 100.00)]
        public Decimal HourlyRate { get; set; }

        public ProjectRole GetProjectRoleInstance()
        {
            return new ProjectRole
            {
                Id = this.Id,
                PersonId = this.PersonId,
                ProjectId = this.ProjectId,
                RoleId = this.RoleId,
                HourlyRate = this.HourlyRate
            };
        }

    }
}
