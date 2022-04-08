using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used for the person report view
    /// </summary>
    public class PersonReportVM
    {
        public int Id { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Number Of Projects")]
        public int NumProjects { get; set; }
        public string Name { get; set; }
        [DisplayName("Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [DisplayName("Total Hourly")]
        public decimal TotalHourly { get; set; }
        [DisplayName("Role(s)")]
        public string Role { get; set; }
    }
}
