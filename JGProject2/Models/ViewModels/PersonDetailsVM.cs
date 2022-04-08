using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to display details about a person
    /// </summary>
    public class PersonDetailsVM
    {
        public int Id { get; set; }
        [DisplayName("First Name"), MaxLength(30)]
        public string FirstName { get; set; }
        [DisplayName("Middle Name"), MaxLength(30)]
        public string MiddleName { get; set; }
        [DisplayName("Last Name"), MaxLength(30)]
        public string LastName { get; set; }
        [DisplayName("Email"), EmailAddress]
        public string Email { get; set; }
    }
}
