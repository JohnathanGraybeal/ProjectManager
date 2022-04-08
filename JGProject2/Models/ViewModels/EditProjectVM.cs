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
    /// View model used to edit a project
    /// </summary>
    public class EditProjectVM
    {
        public int Id { get; set; }
        [DisplayName("Project Name"), Required, MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Start Date"), DataType(DataType.Date), Required]
        public DateTime StartDate { get; set; }
        [DisplayName("Due Date"), DataType(DataType.Date), Required]
        public DateTime DueDate { get; set; }
        [DisplayName("Project Roles")]
        public List<ProjectRole> ProjectRoles { get; set; }

        public Project GetProjectInstance()
        {
            return new Project
            {
                Id = this.Id,
                Name = this.Name,
                StartDate = this.StartDate,
                DueDate = this.DueDate,
               
                

            };
            


        }
    }
}
