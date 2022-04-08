using JGProject2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    public class ProjectReportVM
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Number Of People")]
        public int NumPeople { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Hourly Rate"), DisplayFormat(DataFormatString = "{0:#.####}") ]
        public decimal HourlyRate { get; set; }
        [DisplayName("Total Project Hourly Rate")]
        public decimal TotalHourly { get; set; }
        [DisplayName("Project Role(s)")]
        public string Role { get; set; }
    }
}
