using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
   /// <summary>
   /// View model used to display information about a project
   /// </summary>
    public class ProjectDetailsVM
    {
        public int Id { get; set; }
        [DisplayName("Project Name"), MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Start Date"), DataType(DataType.Date)]
        public string PersonName { get; set; }
        public DateTime StartDate { get ; set; }
        [DisplayName("Due Date"), DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [DisplayName("Number of People Assigned")]
        public int NumPeopleAssigned { get; set; }
        public List<Person> PeopleAssigned { get; set; }
        [DisplayName("Number of Roles")]
        public int NumRoles { get; set; }
        public string Roles { get; set; }
        [DisplayName("Days Until Due")]
        public int DaysDue { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }

        
    }
}
