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
    /// Viewmodel used to create a new person
    /// </summary>
    public class CreatePersonVM
    {
        [DisplayName("First Name"), Required, MaxLength(30)]
        public string FirstName { get; set; }
        private string _MiddleName;
        [DisplayName("Middle Name"), MaxLength(30)]
        public string MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
                if (value == null)
                {
                    _MiddleName = "";
                }
                else
                {
                    _MiddleName = value;
                }
            }
        }
        [DisplayName("Last Name"), Required, MaxLength(30)]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address"), Required]
        public string Email { get; set; }

        //uses view model to get instance of person
        public Person GetPersonInstance()
        {
            return new Person
            {
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Email = this.Email

            };
        }
        
    }
}
