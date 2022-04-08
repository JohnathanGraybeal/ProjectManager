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
    /// View model used to edit a person
    /// </summary>
    public class EditPersonVM
    {
        public int Id { get; set; }
        [DisplayName("First Name"), Required, MaxLength(30)]
        public string FirstName { get; set; }
        [DisplayName("Middle Name"), MaxLength(30)]
        public string MiddleName { get; set; }
        [DisplayName("Last Name"), Required, MaxLength(30)]
        public string LastName { get; set; }
        [DisplayName("Email"), EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public Person GetInstanceOf()
        {
            return new Person
            {
                Id = this.Id,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Email = this.Email

            };

            
        }
    }
}
