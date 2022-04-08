using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.Entities
{
    /// <summary>
    /// Model for people
    /// </summary>
    public class Person
    {
      
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string MiddleName { get; set; }
        [Required, MaxLength(30)]
        public string LastName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        public List<ProjectRole> ProjectRoles { get; set; }

        public Person()
        {
            
            ProjectRoles = new List<ProjectRole>();
           
        }


    }
}
