using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JGProject2.Models.ViewModels
{
    /// <summary>
    /// View model used to delete a person
    /// </summary>
    public class DeletePersonVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }



    }
}
